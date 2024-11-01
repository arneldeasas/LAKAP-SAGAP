
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;


namespace LAKAPSAGAP.Services.Core
{
    public class AuthRepository : IAuthRepository
    {
        [Inject] NavigationManager NavigationManager { get; set; }
		private readonly MyDbContext _context;
        private readonly UserManager<UserAuth> _userManager;


		public AuthRepository(MyDbContext context,UserManager<UserAuth> userManager, SignInManager<UserAuth> signInManager
            )
        {
            _context = context;
			_userManager = userManager;
		}

        public async Task Authenticate(LoginViewModel login,IHttpContextAccessor httpContext)
		{
			try
            {
				UserAuth userAuth = await GetAuthUser(login.Username);



				var user = await _userManager.FindByNameAsync(login.Username);
				var userRoles = await _userManager.GetRolesAsync(user);
				if (user == null)
				{
					throw new Exception("User not found.");
				}
				var result = await _userManager.CheckPasswordAsync(user, login.Password);
				if (!result) throw new Exception("Invalid password.");

				var claims = new List<Claim> {
							 new Claim(ClaimTypes.Name, userAuth.UserName)

				};
				foreach (var role in userRoles)
				{
					claims.Add(new Claim(ClaimTypes.Role, role));
				}
				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

				await httpContext.HttpContext.SignInAsync( claimsPrincipal);

				//NavigationManager.NavigateTo("/Users");
			}	
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
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
    }
}
