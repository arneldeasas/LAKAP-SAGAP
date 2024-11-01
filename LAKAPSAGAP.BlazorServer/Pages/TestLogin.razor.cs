
using Microsoft.AspNetCore.Antiforgery;

namespace LAKAPSAGAP.BlazorServer.Pages
{
    public partial class TestLogin
    {
        string? errorMessage = string.Empty;
        [CascadingParameter]
        private HttpContext HttpContext { get; set; }
		[Inject] NavigationManager navManager { get; set; }
        [Inject] IAuthRepository authService { get; set; }
        [Inject] IAntiforgery Antiforgery { get; set; }

		[SupplyParameterFromForm]
        LoginViewModel loginModel { get; set; } = new();

      
        async Task Login()
        {

            try
            {
				await authService.Authenticate(loginModel);

			}
			catch (Exception)
            {

                throw;
            }
             
        }
    }
}