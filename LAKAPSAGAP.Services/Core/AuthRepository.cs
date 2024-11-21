
using System.Security.Claims;
using LAKAPSAGAP.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
  
        public AuthRepository(MyDbContext context, UserManager<UserAuth> userManager, SignInManager<UserAuth> signInManager, NavigationManager navigationManager, HttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _navigationManager = navigationManager;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<bool> Authenticate(LoginViewModel login)
        {

            //    UserAuth userAuth = await GetAuthUser(login.Username);

            try
            {
				var user = await _userManager.FindByNameAsync(login.Username);
				//var userRoles = await _userManager.GetRolesAsync(user);
				if (user == null)
				{
					throw new Exception("User not found.");
				}
				var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, true, false);
                
                // Notify the AuthenticationStateProvider that the user is authenticated
   
                Console.WriteLine(_contextAccessor.HttpContext.User);
                var name = _contextAccessor.HttpContext.User;

                Console.WriteLine(result);
                return result.Succeeded;

			}
			catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        //public async Task<ClaimsPrincipal> MakeNewAuthenticatedUser(LoginViewModel login)
        //{
        //    try
        //    {
        //        var userAuth = _context.UserAuth.FirstOrDefault(x => x.UserName == login.Username);
        //        var roleId = _context.UserInfo.Where(x => x.UserAuthId == userAuth.Id).FirstOrDefault().RoleId;
        //        var roleName = _context.Role.Where(x => x.Id == roleId).FirstOrDefault().NormalizedName;
        //        if (userAuth == null)
        //        {
        //            throw new Exception("User not found.");
        //        }
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, userAuth.UserName),
        //            new Claim(ClaimTypes.Email, userAuth.Email),
        //            new Claim(ClaimTypes.NameIdentifier, userAuth.Id),
        //            // Custom claim example
        //            new Claim(ClaimTypes.Role, roleName)    // Example role-based claim

        //        };



        //        var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var principal = new ClaimsPrincipal(userIdentity);

        //        return principal;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public async Task<UserAuth> GetAuthUserByUsername(string username)
        {
            var user = await _context.UserAuths.FirstOrDefaultAsync(x => x.UserName == username);
            Console.WriteLine(user);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return user;
        }

		public async Task<UserAuth> GetAuthUserByUserAuthId(string UserAuthId)
		{
			var user = await _context.UserAuths.FirstOrDefaultAsync(x => x.Id == UserAuthId);
			Console.WriteLine(user);
			if (user == null)
			{
				throw new Exception("User not found.");
			}
			return new UserAuth
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
		}

		public UserInfo GetAuthenticatedUser()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.UserInfos.Find(userId);

            return user;
        }
		
    }
}
