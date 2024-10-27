
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Configuration;


namespace LAKAPSAGAP.Services.Core
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        private readonly UserManager<UserAuth> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public static readonly string _uploadPath = Path.Combine("wwwroot", "attachments");
        public static readonly int maxFileSize = 1024 * 1024 * 5;
        public UserRepository(MyDbContext context, UserManager<UserAuth> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task CreateUser(CreateAccountViewModel account)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    UserAuth? duplicate = await _context.UserAuth.FirstOrDefaultAsync(x => x.UserName == account.Username);

                    if (duplicate is not null) throw new Exception("Account already exists");

                    var userAuth = new UserAuth
                    {
                        UserName = account.Username,
                        Email = account.Email,
                    };
                    Console.WriteLine(account.UserRole);
                    var role = account.UserRole.ToUpper();
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(account.UserRole.ToUpper()));
                    }

                    var result = await _userManager.CreateAsync(userAuth, account.Password);
                    Console.WriteLine(result);
                    if (!result.Succeeded) throw new Exception("Failed to create account");

                    await _userManager.AddToRoleAsync(userAuth, role);
                    UserInfo userInfo = new UserInfo
                    {
                        UserAuthId = userAuth.Id,
                        FirstName = account.FirstName,
                        MiddleName = account.MiddleName,
                        LastName = account.LastName,
                        Barangay = account.Barangay,
                        Email = account.Email,
                        Phone = account.Phone,
                        UserRole = account.UserRole
                    };
                    _context.UserInfo.Add(userInfo);
                    _context.SaveChanges();
                    
                    //Makes metadata for files
                    var fileMetadataList = account.fileList.Select(x => new FileMetadata(x, userInfo.Id)).ToArray();

                    var attachmentUrlList = await UploadAttachments(fileMetadataList);

                    foreach (var attachmentUrl in attachmentUrlList)
                    {
                        var attachment = new UserInfo.Attachment
                        {
                            UserId = userInfo.Id,
                            Url = attachmentUrl
                        };
                        _context.Attachment.Add(attachment);
                    }
                
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    throw;
                }
                
            }
        }

        public async Task<List<string>> UploadAttachments(FileMetadata[] fileMetadataList)
        {
            if (fileMetadataList.Any(x => x.File.Size > maxFileSize))
            {
                throw new Exception("File size too large. Try uploading files lower than 5 mb.");
            }

          
            var uploadTasks = fileMetadataList.Select(file => UploadFileAsync(file)
            ).ToList();

            await Task.WhenAll(uploadTasks);

            return fileMetadataList.Select(x => x.FilePath).ToList();
        }

        private async Task<string> UploadFileAsync(FileMetadata file)
        {
            try
            {
                using (var fileStream = new FileStream(file.FilePath, FileMode.Create))
                {
                    
                    await file.File.OpenReadStream(maxFileSize).CopyToAsync(fileStream);
                }
                return file.FilePath; 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to upload {file.File.Name}: {e.Message}");
                throw; 
            }
        }
        public async Task<UserInfo> GetUser(int id)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("No user found.");
        }
    }
}

public class FileMetadata
{
    public IBrowserFile File { get; set; }
    public int UserId { get; set; }
    private readonly string _filePath;
    public string FilePath => _filePath;

    public FileMetadata(IBrowserFile file, int userId)
    {
        File = file;
        UserId = userId;
        _filePath = Path.Combine(UserRepository._uploadPath, $"{UserId}_{file.Name}");
    }
}