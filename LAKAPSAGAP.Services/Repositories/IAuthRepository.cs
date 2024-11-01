﻿
using Microsoft.AspNetCore.Http;

namespace LAKAPSAGAP.Services.Repositories
{
    public interface IAuthRepository
    {
        public Task Authenticate(LoginViewModel login,IHttpContextAccessor httpContext);
        public Task<UserAuth> GetAuthUser(string username);
    }
}
