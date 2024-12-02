
namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsCreateUpdate
{

    [Inject] DialogService _dialogService { get; set; }
    [Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;

    List<KitViewModel> Kits { get; set; } = new();
    public KitViewModel Kit { get; set; } = new();
    public List<ReliefReceivedViewModel> Items { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
        }
    }

    Task SubmitKit()
    {
        return Task.CompletedTask;
    }

}
