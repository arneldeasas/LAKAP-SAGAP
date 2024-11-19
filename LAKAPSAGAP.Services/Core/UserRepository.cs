using LAKAPSAGAP.Services.Core.Helpers;

namespace LAKAPSAGAP.Services.Core;

public class UserRepository(
	MyDbContext context,
	UserManager<UserAuth> userManager,
	RoleManager<IdentityRole> roleManager,
	AuthRepository authRepository,
	UserAttachmentRepository userAttachmentRepository) : IUserRepository
{
	public MyDbContext _context = context;
	private readonly UserManager<UserAuth> _userManager = userManager;
	private readonly RoleManager<IdentityRole> _roleManager = roleManager;
	private readonly AuthRepository _authRepository = authRepository;
	private readonly UserAttachmentRepository _userAttachmentRepository = userAttachmentRepository;

	public async Task<UserInfo> CreateUser(CreateAccountViewModel account)
	{

		using var transaction = _context.Database.BeginTransaction();
		try
		{

			UserAuth? duplicate = await _context.UserAuth.FirstOrDefaultAsync(x => x.UserName == account.Username);

			if (duplicate is not null) throw new Exception("Account already exists");

			var userAuth = new UserAuth
			{
				UserName = account.Username,
				Email = account.Email,
			};

			var role = _context.Role.Find(account.RoleId).Name.ToUpper();
			if (!await _roleManager.RoleExistsAsync(role))
			{
				await _roleManager.CreateAsync(new IdentityRole(role));
			}

			var result = await _userManager.CreateAsync(userAuth, account.Password);
			if (!result.Succeeded) throw new Exception("Failed to create account");

			await _userManager.AddToRoleAsync(userAuth, role);

			int existingRecordsCount = await _context.GetCount<UserInfo>();
			UserInfo userInfo = new()
			{
				Id = IdGenerator.GenerateId(IdGenerator.PFX_USERINFO, existingRecordsCount),
				UserAuthId = userAuth.Id,
				FirstName = account.FirstName,
				MiddleName = account.MiddleName,
				LastName = account.LastName,
				Barangay = account.Barangay,
				Email = account.Email,
				Phone = account.Phone,
				RoleId = account.RoleId
			};
			await _context.Create(userInfo);
			await _userAttachmentRepository.UploadAttachments(account.fileList, userInfo.Id);

			transaction.Commit();
			return userInfo;
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			transaction.Rollback();
			throw;
		}
	}

	public async Task<UserInfo> UpdateUser(CreateAccountViewModel data, List<(string Id, string Url)> filesToDelete)
	{
		using var transaction = _context.Database.BeginTransaction();

		try
		{
			Console.WriteLine(data.Id);
			var userAuth = await _context.UserAuth.Where(x => x.Id == data.UserAuthId).SingleOrDefaultAsync();

			if (!String.IsNullOrEmpty(data.Password)) { var updatedUserAuth = await _userManager.ChangePasswordAsync(userAuth, userAuth.PasswordHash, data.Password); }
			if (!String.IsNullOrEmpty(data.Username)) { var updatedUserAuth = await _userManager.SetUserNameAsync(userAuth, data.Username); }


			var userInfo = await _context.UserInfo.Where(x => x.Id == data.Id).SingleOrDefaultAsync();


			if (userInfo is not null)
			{
				userInfo.Id = data.Id;
				userInfo.RoleId = data.RoleId;
				userInfo.FirstName = data.FirstName;
				userInfo.LastName = data.LastName;
				userInfo.MiddleName = data.MiddleName;
				userInfo.Barangay = data.Barangay;
				userInfo.Email = data.Email;
				userInfo.Phone = data.Phone;

				await _context.UpdateItem<UserInfo>(userInfo);

				//await _context.UpdateMany<List<Attachment>>()

				foreach (var toDelete in filesToDelete)
				{
					var result = await _userAttachmentRepository.DeleteAttachmentSoft(toDelete.Id);
				}

				await _userAttachmentRepository.UploadAttachments(data.fileList, userInfo.Id);
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();
				return userInfo;
			}
			else
			{
				throw new Exception("User not Found");
			}
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}

	}
	public async Task<UserInfo> DeleteUser(string Id)
	{
		try
		{
			var userInfo = await _context.GetById<UserInfo>(Id);
			userInfo.IsDeleted = true;

			userInfo = await _context.UpdateItem<UserInfo>(userInfo);
			return userInfo;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<UserInfo> ArchiveUser(string Id)
	{
		try
		{
			var userInfo = await _context.GetById<UserInfo>(Id);
			userInfo.isArchived = true;

			userInfo = await _context.UpdateItem<UserInfo>(userInfo);
			return userInfo;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<List<IdentityRole>> GetUserRoles()
	{
		try
		{
			return await _context.Role.ToListAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<List<UserInfo>> GetAllUsers()
	{
		try
		{
			return await _context.UserInfo
					.Include(u => u.Role)
					.Where(u => u.isArchived == false)
					.ToListAsync();
		}
		catch (Exception)
		{

			throw;
		}
	}
}

