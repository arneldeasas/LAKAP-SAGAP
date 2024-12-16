using Microsoft.AspNetCore.Components.Authorization;

namespace LAKAPSAGAP.BlazorServer.Pages.UserManagement
{
	public partial class Users
	{

		[Inject] DialogService _dialogService { get; set; }
		[Inject] IUserRepository _userRepo { get; set; }
		[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
		//[Inject] HttpContextAccessor HttpContextAccessor { get; set; }

		public RadzenDataGrid<UserInfo> UsersDG;

		private List<BreadcrumbViewModel> Breadcrumbs = new()
		{
		new BreadcrumbViewModel { Path = "/Users", Text = "User Management" },
		};

		private List<UserInfo> _userInfoList = new();
		private List<UserInfo> tableData = new();

		public string search = string.Empty;

		protected override async Task OnInitializedAsync()
		{
			_userInfoList = await _userRepo.GetAllUsers();
			tableData = _userInfoList;
		}

		private void SearchUsers(string value)
		{
			if (value != string.Empty)
			{
				tableData = _userInfoList.Where(x => x.Id.ToString().ToLower().Contains(value.ToLower())
				   || x.FirstName.ToString().ToLower().Contains(value.ToLower())
				   || x.LastName.ToString().ToLower().Contains(value.ToLower())
				   || x.MiddleName.ToString().ToLower().Contains(value.ToLower())
				   || x.Barangay.ToString().ToLower().Contains(value.ToLower())
				   || x.Email.ToString().ToLower().Contains(value.ToLower())
				   || x.Phone.ToString().ToLower().Contains(value.ToLower())
				   || x.Role.ToString().ToLower().Contains(value.ToLower())).ToList();
			}
			else
			{
				tableData = _userInfoList;
			}
		}

		public async Task ArchiveUser(string userId)
		{
			if (!await _jSRuntime.InvokeAsync<bool>("Confirmation", null, "warning")) return;
			await _userRepo.ArchiveUser(userId);
			_userInfoList = await _userRepo.GetAllUsers();
			tableData = _userInfoList;
			StateHasChanged();
			await _jSRuntime.InvokeAsync<bool>("Confirmation", "User Account Archived Successfully.", "success", null);
		}

	}
}
