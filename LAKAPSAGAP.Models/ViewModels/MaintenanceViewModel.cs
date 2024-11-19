namespace LAKAPSAGAP.Models.ViewModels;

public class StockCategoryViewModel
{
    public string Name { get; set; }
}

public class StockItemViewModel
{

    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string UoMId { get; set; }
}

public class FloorViewModel
{
    public string Name { get; set; }
    public List<RackViewModel> RackList { get; set; } = new();
}