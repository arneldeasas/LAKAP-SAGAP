
namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsCreateUpdate
{

    bool Loading = true;

    public KitViewModel Kit { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Loading = false;
            StateHasChanged();
        }
    }


}
