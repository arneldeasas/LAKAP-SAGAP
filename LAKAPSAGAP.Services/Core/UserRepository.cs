


using LAKAPSAGAP.Models.ViewModel;
using LAKAPSAGAP.Services.Core.Helpers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Configuration;


namespace LAKAPSAGAP.Services.Core
{
    public class UserRepository :  IUserRepository
	{
        public  MyDbContext _context;
        private readonly UserManager<UserAuth> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthRepository _authRepository;
        private readonly UserAttachmentRepository _userAttachmentRepository;
        public static string UserIdPrefix = "ACC_";
        
        public UserRepository(MyDbContext context, 
            UserManager<UserAuth> userManager, 
            RoleManager<IdentityRole> roleManager, 
            AuthRepository authRepository, 
            UserAttachmentRepository userAttachmentRepository) 
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _authRepository = authRepository;
            _userAttachmentRepository = userAttachmentRepository;
        }

       
        public async Task<UserInfo> CreateUser(CreateAccountViewModel account)
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
                    Console.WriteLine(account);

                    var role = _context.Role.Find(account.RoleId).Name.ToUpper();
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }

                    var result = await _userManager.CreateAsync(userAuth, account.Password);
                    Console.WriteLine(result);
                    if (!result.Succeeded) throw new Exception("Failed to create account");

                    await _userManager.AddToRoleAsync(userAuth, role);

                 //  var addedBy = _authRepository.GetAuthenticatedUser();
                   // string? addedById = addedBy != null ? addedBy.LastModifiedById : null;
                    int existingRecordsCount = await _context.GetCount<UserInfo>();
                    UserInfo userInfo = new UserInfo
                    {
                        Id = IdGenerator.GenerateId(IdGenerator.PFX_USERINFO,existingRecordsCount),
                        UserAuthId = userAuth.Id,
                        FirstName = account.FirstName,
                        MiddleName = account.MiddleName,
                        LastName = account.LastName,
                        Barangay = account.Barangay,
                        Email = account.Email,
                        Phone = account.Phone,
                        RoleId = account.RoleId,
                      

                    };
                    await _context.Create<UserInfo>(userInfo);
                    Console.WriteLine(userInfo);
                    await _userAttachmentRepository.UploadAttachments(account.fileList, userInfo.Id);

                    transaction.Commit();
                    return userInfo;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public async Task<UserInfo> UpdateUser(UserInfoViewModel userInfoViewModel)
        {
            try
            {
                var updatedUserInfo = new UserInfo {
                    RoleId = userInfoViewModel.RoleId,
                    FirstName = userInfoViewModel.FirstName,    
                    LastName = userInfoViewModel.LastName,
                    MiddleName = userInfoViewModel.MiddleName,
                    Barangay = userInfoViewModel.Barangay,
                    Email = userInfoViewModel.Email,
                    Phone = userInfoViewModel.Phone,
                };
                updatedUserInfo = await _context.UpdateItem<UserInfo>(updatedUserInfo);
                return updatedUserInfo;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<UserInfo> DeleteUser(string Id)
        {
            try
            {
                var userInfo = await _context.GetById<UserInfo>(Id);
                userInfo.IsDeleted = true;
      
                userInfo = await _context.UpdateItem<UserInfo>(userInfo);
                return userInfo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserInfo> ArchiveUser(string Id)
        {
            try
            {
				var userInfo = await _context.GetById<UserInfo>(Id);
				userInfo.isArchived = true;

				userInfo = await _context.UpdateItem<UserInfo>(userInfo);
				return userInfo;
			}
            catch (Exception)
            {

                throw;
            }
        }

		public async Task<List<IdentityRole>> GetUserRoles()
		{
			try
			{
				return await _context.Role.ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

        public async Task<List<UserInfo>> GetAllUsers()
        {
            try
            {
                return await _context.UserInfo
                        .Include(u => u.Role)
                        .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
	}
}

