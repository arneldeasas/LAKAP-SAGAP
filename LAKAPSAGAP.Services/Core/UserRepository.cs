﻿


using LAKAPSAGAP.Services.Core.Helpers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Configuration;


namespace LAKAPSAGAP.Services.Core
{
    public class UserRepository : CommonRepository<UserInfo>, IUserRepository
    {

        private readonly UserManager<UserAuth> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthRepository _authRepository;
        private readonly UserAttachmentRepository _userAttachmentRepository;
        public static string UserIdPrefix = "ACC_";
        public UserRepository(MyDbContext context, 
            UserManager<UserAuth> userManager, 
            RoleManager<IdentityRole> roleManager, 
            AuthRepository authRepository, 
            UserAttachmentRepository userAttachmentRepository) : base(context)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _authRepository = authRepository;
            _userAttachmentRepository = userAttachmentRepository;
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
                    Console.WriteLine(account);
                    var role = account.UserRole.ToUpper();
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(account.UserRole.ToUpper()));
                    }

                    var result = await _userManager.CreateAsync(userAuth, account.Password);
                    Console.WriteLine(result);
                    if (!result.Succeeded) throw new Exception("Failed to create account");

                    await _userManager.AddToRoleAsync(userAuth, role);

                 //  var addedBy = _authRepository.GetAuthenticatedUser();
                   // string? addedById = addedBy != null ? addedBy.LastModifiedById : null;
                    int existingRecordsCount = await GetCount();
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
                        UserRole = account.UserRole,
                      

                    };
                    await Create(userInfo);
                    Console.WriteLine(userInfo);
                    await _userAttachmentRepository.UploadAttachments(account.fileList, userInfo.Id);

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

        public async Task<UserInfo> DeleteUser(string Id)
        {
            try
            {
                var userInfo = _context.UserInfo.First(x=> x.Id == Id && x.IsDeleted );
                if (userInfo is not null) throw new Exception("User already deleted.");
                userInfo.IsDeleted = true;
                await Update(userInfo);
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
                var userInfo = _context.UserInfo.First(x=> x.Id == Id && x.isArchived);
                if (userInfo is not null) throw new Exception("User already archived.");

                userInfo.isArchived = true;
                await Update(userInfo);
                return userInfo;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
