namespace LAKAPSAGAP.BlazorServer.Pages.UserManagement
{
	public partial class Users
	{
		[Inject] DialogService _dialogService { get; set; }
		[Inject] IUserRepository _userRepo { get; set; }

		[CascadingParameter]
		private HttpContext HttpContext { get; set; }

		public RadzenDataGrid<UserInfo> UsersDG;

		private List<BreadcrumbViewModel> Breadcrumbs = new()
		{
		new BreadcrumbViewModel { Path = "/Users", Text = "User Management" },
		};

		private List<UserInfo> _userInfoList = new();

		protected override async Task OnInitializedAsync()
		{
			_userInfoList = await _userRepo.GetAllUsers();
		}
	}
}
