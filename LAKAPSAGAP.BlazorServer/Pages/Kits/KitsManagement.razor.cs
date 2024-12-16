
using LAKAPSAGAP.Models.Models;
using Mapster;

namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsManagement
{
    [Inject] IPackedReliefKitRepository _packedReliefKitRepository { get; set; }
    [Inject] DialogService _dialogService { get; set; }
    [Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;

    List<KitViewModel> KitComponentList { get; set; } = new List<KitViewModel>();
    List<PackedReliefKitViewModel> PackedReliefList { get; set; } = new List<PackedReliefKitViewModel>();

    RadzenDataGrid<PackedReliefKitViewModel> PackedReliefListDG { get; set; } = default!;

    public bool Loading = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Loading = false;
            var response = await _packedReliefKitRepository.GetAllPackedReliefKitAsync();
            PackedReliefList = response.Adapt<List<PackedReliefKitViewModel>>();
            StateHasChanged();
        }
    }

    async Task Archive(PackedReliefKitViewModel content)
    {
        if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;
        var p = content.Adapt<PackedReliefKit>();
        await _packedReliefKitRepository.ArchivePackedReliefKitAsync(p);
        await _jSRuntime.InvokeVoidAsync("Toast", "success", $"Packing Report archived successfully!");
        await PackedReliefListDG.Reload();
        StateHasChanged();
    }

    static KitsManagement()
    {
        TypeAdapterConfig<Floor, FloorViewModel>.NewConfig()
            .Map(d => d.RackList, s => s.Racks);

        TypeAdapterConfig<FloorViewModel, Floor>.NewConfig()
            .Map(d => d.Racks, s => s.RackList);

        TypeAdapterConfig<PackedReliefKitViewModel, PackedReliefKit>.NewConfig()
            .Map(d => d.KitId, s => s.KitType);
        
        TypeAdapterConfig<PackedReliefKit, PackedReliefKitViewModel >.NewConfig()
            .Map(d => d.KitType, s => s.KitId)
            .Map(d => d.KitName, s => s.Kit.Name)
            .Map(d => d.FloorName, s => s.Floor.Name)
            .Map(d => d.RackName, s => s.Rack.Name);

        TypeAdapterConfig<PackedReliefKitViewModel, PackedReliefKit>.NewConfig()
            .Map(d => d.KitId, s => s.KitType)
            .Map(d => d.Kit.Name, s => s.KitName)
            .Map(d => d.Floor.Name, s => s.FloorName)
            .Map(d => d.Rack.Name, s => s.RackName);
    }

}
