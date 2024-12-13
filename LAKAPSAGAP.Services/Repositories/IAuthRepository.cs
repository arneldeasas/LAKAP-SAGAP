

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LAKAPSAGAP.Services.Repositories
{
    public interface IAuthRepository
    {
       
        public Task<string> Authenticate(LoginViewModel login);
       // public Task<ClaimsPrincipal?> MakeNewAuthenticatedUser(LoginViewModel login);
		Task<UserAuth> GetAuthUserByUsername(string username);
		Task<UserAuth> GetAuthUserByUserAuthId(string UserAuthId);
        Task<UserInfo> GetUserInfoByUserAuthId(string userAuthId);
        Task<bool> Logout();
		//public UserInfo GetAuthenticatedUser();
	}
}
