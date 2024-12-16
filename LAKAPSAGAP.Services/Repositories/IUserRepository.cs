namespace LAKAPSAGAP.Services.Repositories;

public interface IUserRepository
{

	public Task<UserInfo> CreateUser(CreateAccountViewModel user);
	public Task<UserInfo> UpdateUser(CreateAccountViewModel data, List<(string Id, string Url)> filesToDelete);
	public Task<UserInfo> DeleteUser(string Id); //mark as deleted
	public Task<UserInfo> ArchiveUser(string Id);
	public Task<List<IdentityRole>> GetUserRoles();
	public Task<List<UserInfo>> GetAllUsers();
}
