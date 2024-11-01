
namespace LAKAPSAGAP.BlazorServer.Pages
{
    public partial class TestLogin
    {
        string? errorMessage = string.Empty;
        [Inject] NavigationManager navManager { get; set; }
        [Inject] IAuthRepository authService { get; set; }

        [SupplyParameterFromForm]
        LoginViewModel loginModel { get; set; } = new();

        async Task Login()
        {
            try
            {
               // await authService.Authenticate(loginModel);
                navManager.NavigateTo("/");
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }
    }
}