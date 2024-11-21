namespace LAKAPSAGAP.Models.Models;

public class Category : CommonModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public List<StockItem> StockItems { get; set; }
}
