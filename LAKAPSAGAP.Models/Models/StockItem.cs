namespace LAKAPSAGAP.Models.Models;

public class StockItem : CommonModel //record of all Relief Items
{
	public string Name { get; set; }
	public string CategoryId { get; set; }
	public Category Category { get; set; }
	public string UoMId { get; set; }
	public UoM UoM { get; set; }
  
    public List<StockDetail> StockDetailList { get; set; }
}
