namespace LAKAPSAGAP.Models.Models;

public class Warehouse : CommonModel
{
	public string Name { get; set; }
	public string Location { get; set; }
	public List<ReliefReceived> ReliefReceivedList { get; set; }
	public List<Floor> FloorList { get; set; }
}
