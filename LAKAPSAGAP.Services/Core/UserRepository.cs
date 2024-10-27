using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.Services.Core
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        private readonly UserManager<UserAuth> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(MyDbContext context, UserManager<UserAuth> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateUser(CreateAccountViewModel account)
        {

            Console.WriteLine(account);
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
                _roleManager.CreateAsync(new IdentityRole(account.UserRole.ToUpper()));
            }

            var result = await _userManager.CreateAsync(userAuth, account.Password);
            Console.WriteLine(result);
            if (!result.Succeeded) throw new Exception("Failed to create account");

            await _userManager.AddToRoleAsync(userAuth, role);
            Console.WriteLine(userAuth);
            //UserAuth userAuth = new()
            //{
            //    Username = account.Username,
            //    Hash = BCrypt.Net.BCrypt.HashPassword(account.Password)
            //};
            //await _context.UserInfos.AddAsync(user);
            //await _context.SaveChangesAsync();
            //return user;
        }

        public async Task<UserInfo> GetUser(int id)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("No user found.");
        }
    }
}
