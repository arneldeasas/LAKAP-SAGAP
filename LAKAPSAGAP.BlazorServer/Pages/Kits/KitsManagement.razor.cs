
namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsManagement
{
    List<KitViewModel> KitComponentList { get; set; } = new List<KitViewModel>();
    public bool Loading = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //if (firstRender)
        //{
        //    Loading = false;
        //    StateHasChanged();
        //}
    }
}
