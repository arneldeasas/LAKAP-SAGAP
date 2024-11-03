using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.BlazorServer.Pages.Security
{
    public partial class AuthLogin
    {
		[Inject] IHttpContextAccessor HttpContextAccessor { get; set; }
		[Inject] NavigationManager NavigationManager { get; set; }
      
        [Inject] IAuthRepository authRepository { get; set; }
        LoginViewModel loginViewModel = new();
        string loginError = string.Empty;
        
        async Task Login()
        {
            try
            {
            
                 await authRepository.Authenticate(loginViewModel);
				
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