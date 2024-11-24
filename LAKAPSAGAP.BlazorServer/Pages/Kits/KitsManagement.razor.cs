
namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsManagement
{
    public bool Loading = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Loading = false;
            StateHasChanged();
        }
    }
}
