using Microsoft.AspNetCore.Identity;
using Radzen.Blazor.Rendering;
using System.Net.Http;
namespace LAKAPSAGAP.BlazorServer.Shared;

public partial class NavMenu
{
    
    [Inject] private NavigationManager _navManager { get; set; }
    [Inject] SignInManager<UserAuth> _signInManager { get; set; }
    [Inject] IAuthRepository _authRepository { get; set; }
	[Inject] IJSRuntime _jsRuntime { get; set; }
    [Inject] HttpClient HttpClient { get; set; }
	[Parameter]
    public UserInfoViewModel user { get; set; }
    ElementReference masterDataButton { get; set; }
    ElementReference warehouseButton { get; set; }

    Popup masterDataPopup { get; set; }
    Popup warehousePopup { get; set; }

    bool _isVisible { get; set; } = false;

    string uri { get; set; } = String.Empty;
  
    

    private bool ToggleVisibility()
    {
        return _isVisible = !_isVisible;
    }

    private bool UriHas(string keyword)
    {
        uri = _navManager.Uri;
        return uri.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }

    async Task Logout()
    {
        try
        {
			var response = await HttpClient.PostAsync(new Uri("https://localhost:7224/api/account/logout"), null);
            if (response.IsSuccessStatusCode)
            {
                _navManager.NavigateTo("/",true);
			}
		}
        catch (Exception)
        {

            throw;
        }
	}

}