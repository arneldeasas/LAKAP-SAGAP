using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace LAKAPSAGAP.BlazorServer.Pages.Security;

public partial class AuthLogin
{
	[CascadingParameter] HttpContext HttpContext { get; set; }

	[Inject] NavigationManager NavigationManager { get; set; }
	[Inject] IAuthRepository authRepository { get; set; }
	[Inject] IAntiforgery Antiforgery { get; set; }
	[Inject] IJSRuntime JSRuntime { get; set; }

    [SupplyParameterFromForm] LoginViewModel loginViewModel { get; set; } = new();
	string _errorMessage { get; set; } = string.Empty;
	//async Task Login()
	//{
	//    try
	//    {
	//        var httpClient = HttpClientFactory.CreateClient("API");
	//        //var result = await authRepository.Authenticate(loginViewModel);
	//        var result = await httpClient.PostAsJsonAsync("/api/account/login", loginViewModel);

	//    //   var claimsPrincipal = await authRepository.MakeNewAuthenticatedUser(loginViewModel);
	//        //Auth.MarkUserAsAuthenticated(loginViewModel.Username);
	//       var authState = await Auth.GetAuthenticationStateAsync();
	//        var user = authState.User;
	//        Console.WriteLine(authState.User);
	//        if (result.IsSuccessStatusCode) {

	//            NavigationManager.NavigateTo("/users");
	//        } 
	//    }
	//    catch (Exception e)
	//    {
	//        loginError = e.Message;
	//        StateHasChanged();
	//        throw;
	//    }
	//}
	//[IgnoreAntiforgeryToken]
	async Task Login()
	{

		try
		{
			//Console.WriteLine(loginViewModel);
			string roleName = await authRepository.Authenticate(loginViewModel);

			string[] admins = ["CSWD ADMINISTRATION STAFF", "CSWD OFFICE HEAD"];
			if(admins.Contains(roleName.ToUpper()))
			{
                NavigationManager.NavigateTo("/Dashboard");
            }else
			{
                NavigationManager.NavigateTo("/barangay-rep/requests");

            }

        }
		catch (NavigationException)
		{
			throw;
		}
		catch (Exception e)
		{
			_errorMessage = e.Message;
			
        }

	}
}


