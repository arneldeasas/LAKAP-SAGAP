namespace LAKAPSAGAP.BlazorServer.Shared;

public partial class MainLayout
{
    [Inject] HttpContextAccessor _httpContext { get; set; }
    [Inject] IAuthRepository _authRepo { get; set; }
    bool _isBusy { get; set; } = false;
    public UserInfoViewModel? user { get; set; }
    public string? _userId { get; set; }

    protected override void OnInitialized()
    {
        //user ??= new();
        if (string.IsNullOrEmpty(_userId))
        {
            _userId = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        base.OnInitialized();
    }

    protected override async Task OnInitializedAsync()
    {
        if (user is null)
        {
            // _isBusy = true;
            // StateHasChanged();
            await LoadUserInfo();
            // _isBusy = false;
            // StateHasChanged();
        }
    }


    async Task LoadUserInfo()
    {
        UserInfo _user = await _authRepo.GetUserInfoByUserAuthId(_userId);
        if (_user is not null)
        {
            user = new UserInfoViewModel
            {
                FirstName = _user.FirstName,
                MiddleName = _user.MiddleName,
                LastName = _user.LastName,
                RoleName = _user.Role.Name,
            };
        }
    }
}