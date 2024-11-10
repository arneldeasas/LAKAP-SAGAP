using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.BlazorServer.Pages.UserManagement
{
	public partial class UsersView
	{
		[Inject] private IUserRepository? UserRepo { get; set; }
		[Inject] private IUserAttachmentRepository? UserFileRepo { get; set; }
		[Inject] private IAuthRepository? AuthRepo { get; set; }

		[Parameter]
		public DialogService dialogService { get; set; } = default!;

		[Parameter]
		public RadzenDataGrid<UserInfo> datagrid { get; set; } = default!;

		[Parameter]
		public List<UserInfo> datalist { get; set; } = new();

		[Parameter]
		public UserInfo? userData
		{
			get => new UserInfo();
			set
			{
				if (value != null)
				{
					model = new CreateAccountViewModel
					{
						Id = value.Id,
						UserAuthId = value.UserAuthId,
						RoleId = value.RoleId,
						FirstName = value.FirstName,
						MiddleName = value.MiddleName,
						LastName = value.LastName,
						Barangay = value.Barangay,
						Email = value.Email,
						Phone = value.Phone,
						UserRole = value.Role,
					};
				}
			}
		}

		private IJSObjectReference _js { get; set; } = default!;
		private UserInfo? _userData;
		public CreateAccountViewModel model { get; set; } = new();
		string id;
		public List<IdentityRole> userRoles { get; set; }
		public List<(string Id, string Url)> userFiles { get; set; } = new();
		public List<(string Id, string Url)> filesToDelete { get; set; } = new();
		private UserAuth userAuth { get; set; } = new();


		protected override async Task OnInitializedAsync()
		{
			userRoles = await UserRepo.GetUserRoles();
			if (!String.IsNullOrEmpty(model.Id)) userFiles = await UserFileRepo.GetUserAttachments(model.Id);

			if (!String.IsNullOrEmpty(model.Id))
			{
				userAuth = await AuthRepo.GetAuthUserByUserAuthId(model.UserAuthId);
				model.Username = userAuth.UserName;
			}
		}
	}
}
