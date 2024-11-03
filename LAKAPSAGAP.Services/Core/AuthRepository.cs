
using System.Security.Claims;
using LAKAPSAGAP.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;


namespace LAKAPSAGAP.Services.Core
{
    public class AuthRepository : IAuthRepository
    {
        private readonly NavigationManager _navigationManager;
		private readonly MyDbContext _context;
        private readonly UserManager<UserAuth> _userManager;
        private readonly SignInManager<UserAuth> _signInManager;
        private readonly HttpContextAccessor _contextAccessor;
		public AuthRepository(MyDbContext context,UserManager<UserAuth> userManager, SignInManager<UserAuth> signInManager, NavigationManager navigationManager, HttpContextAccessor httpContextAccessor)
        {
            _context = context;
			_userManager = userManager;
			_signInManager = signInManager;
            _navigationManager = navigationManager;
            _contextAccessor = httpContextAccessor;
		}

        public async Task Authenticate(LoginViewModel login)
		{
		
				UserAuth userAuth = await GetAuthUser(login.Username);



				var user = await _userManager.FindByNameAsync(login.Username);
				var userRoles = await _userManager.GetRolesAsync(user);
				if (user == null)
				{
					throw new Exception("User not found.");
				}
                var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, true, false);
               
                Console.WriteLine(result);
                if (result.Succeeded)
                {
                    _navigationManager.NavigateTo("/users");
					throw new InvalidOperationException("can only be used during static rendering.");
				}


        }

        public async Task<UserAuth> GetAuthUser(string username)
        {
            var user = await _context.UserAuth.FirstOrDefaultAsync(x => x.UserName == username);
            
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return user;
        }
        public UserInfo GetAuthenticatedUser()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.UserInfo.Find(userId);

            return user;
        }
    }
}
