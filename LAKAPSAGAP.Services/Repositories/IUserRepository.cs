﻿
namespace LAKAPSAGAP.Services.Repositories
{
    public interface IUserRepository
    {
    
        public Task CreateUser(CreateAccountViewModel user);
        public Task<UserInfo> DeleteUser(string Id); //mark as deleted
        public Task<UserInfo> ArchiveUser(string Id);
        
    }
}