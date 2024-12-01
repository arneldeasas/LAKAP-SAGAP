namespace LAKAPSAGAP.Models.Models;

public class Rack : CommonModel
{
	public string Name { get; set; }
	public string? FloorId { get; set; }
	public Floor Floor { get; set; }

	public List<StockDetail> StockDetailList { get; set; }
}