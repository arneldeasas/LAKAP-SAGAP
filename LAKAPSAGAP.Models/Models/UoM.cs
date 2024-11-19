namespace LAKAPSAGAP.Models.Models;

public class UoM : CommonModel
{
    public string Name { get; set; }
    public string Symbol { get; set; }

    public List<StockItem> StockItems { get; set; }
}
