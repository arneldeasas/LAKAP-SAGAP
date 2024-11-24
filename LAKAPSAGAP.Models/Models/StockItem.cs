namespace LAKAPSAGAP.Models.Models;

public class StockItem : CommonModel //record of all Relief Items
{
	public string CategoryId { get; set; }
	public Category Category { get; set; }
	public string Name { get; set; }
	public string UoMId { get; set; }
	public UoM UoM { get; set; }

	public string FloorId { get; set; }
	public Floor Floor { get; set; }
	public List<StockDetail> StockDetailList { get; set; }
}
