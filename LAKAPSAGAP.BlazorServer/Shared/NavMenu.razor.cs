using Radzen.Blazor.Rendering;

namespace LAKAPSAGAP.BlazorServer.Shared;

public partial class NavMenu
{
    
    [Inject] private NavigationManager _navManager { get; set; }
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

}