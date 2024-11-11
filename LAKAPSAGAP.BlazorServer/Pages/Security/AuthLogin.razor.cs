
namespace LAKAPSAGAP.BlazorServer.Pages.Security
{
    public partial class AuthLogin
    {
        [Inject] IHttpClientFactory HttpClientFactory { get; set; }
   
		[Inject] NavigationManager NavigationManager { get; set; }
      
        [Inject] IAuthRepository authRepository { get; set; }
        LoginViewModel loginViewModel = new();
        string loginError = string.Empty;
        
        async Task Login()
        {
            try
            {
                var httpClient = HttpClientFactory.CreateClient("API");

                var result = await httpClient.PostAsJsonAsync("/api/account/login", loginViewModel);
                if (result.IsSuccessStatusCode) NavigationManager.NavigateTo("/users");
            }
            catch (Exception e)
            {
                loginError = e.Message;
                StateHasChanged();
                throw;
            }
        }
    }
}