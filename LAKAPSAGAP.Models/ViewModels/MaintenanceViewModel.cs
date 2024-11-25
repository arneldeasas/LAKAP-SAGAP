namespace LAKAPSAGAP.Models.ViewModels;

public class FloorViewModel
{
    public string Name { get; set; }
    public List<RackViewModel> RackList { get; set; } = new();
}