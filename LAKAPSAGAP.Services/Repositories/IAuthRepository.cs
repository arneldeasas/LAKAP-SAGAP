
namespace LAKAPSAGAP.Services.Repositories
{
    public interface IAuthRepository
    {
        public Task Authenticate(LoginViewModel login);
    }
}
