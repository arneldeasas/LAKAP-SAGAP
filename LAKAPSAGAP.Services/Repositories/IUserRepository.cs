
namespace LAKAPSAGAP.Services.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUser(CreateAccountViewModel user);
        public Task<UserInfo> GetUser(int id);
    }
}
