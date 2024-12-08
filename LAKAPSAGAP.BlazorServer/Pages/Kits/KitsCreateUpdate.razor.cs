
using Blazored.LocalStorage;
using Mapster;

namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsCreateUpdate
{

    [Inject] DialogService _dialogService { get; set; }
    [Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
    [Inject] ILocalStorageService _localStorageService { get; set; }
    [Inject] IWarehouseRepository? _warehouseRepo { get; set; }
    [Inject] IKittingRepository? _kittingRepo { get; set; }

    List<KitViewModel> Kits { get; set; }
    List<FloorViewModel> Floors { get; set; }
    PackedReliefKitViewModel PackedKit { get; set; }
    public KitViewModel Kit { get; set; }
    public List<ReliefReceivedViewModel> Items { get; set; }
    public FloorViewModel SelectedFloor { get; set; }

    string _whsId;

    protected override async Task OnInitializedAsync()
    {
        _whsId ??= string.Empty;
        Kit ??= new();
        Kits ??= new();
        Items ??= new();
        Floors ??= new();
        PackedKit ??= new();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        if (firstRender)
        {
            _whsId = await _localStorageService.GetItemAsync<string>("whs");
            var whsRes = await _warehouseRepo.GetWarehouseById(_whsId);
            Floors = whsRes.FloorList.Adapt<List<FloorViewModel>>();

            var kitsRes = await _kittingRepo.GetAllKitsAsync();
            Kits = kitsRes.Adapt<List<KitViewModel>>();

            StateHasChanged();
        }
    }

    async Task SubmitKit()
    {
        try
        {

        }
        catch (Exception e)
        {
            await _jSRuntime.InvokeVoidAsync("Toast", "error", $"Something wrong happenned! {e.Message}");
            throw;
        }
    }

    void SetSelectedFloor()
    {
        SelectedFloor = Floors.First(x => x.Id == PackedKit.FloorId);
        StateHasChanged();
    }

    static KitsCreateUpdate()
    {
        TypeAdapterConfig<Floor, FloorViewModel>.NewConfig()
            .Map(d => d.RackList, s => s.Racks);

        TypeAdapterConfig<FloorViewModel, Floor>.NewConfig()
            .Map(d => d.Racks, s => s.RackList);
    }

}
