namespace LAKAPSAGAP.Models.ViewModels;

public class FloorViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string WarehouseId { get; set; }
    public List<RackViewModel> RackList { get; set; } = new();
}