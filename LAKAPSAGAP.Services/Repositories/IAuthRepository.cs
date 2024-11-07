

using Microsoft.AspNetCore.Http;

namespace LAKAPSAGAP.Services.Repositories
{
    public interface IAuthRepository
    {
       
        public Task<bool> Authenticate(LoginViewModel login);
        public Task<UserAuth> GetAuthUser(string username);
        //public UserInfo GetAuthenticatedUser();
    }
}
