using LAKAPSAGAP.Models.Enums;

namespace LAKAPSAGAP.Models.Models;

// GRPO => Relief Received (Batch)
public class ReliefReceived : CommonModel
{
    public AcquisitionTypes AcquisitionType { get; set; }
    public string ReceivedBy { get; set; }
	public string? TruckPlateNumber { get; set; }
    public string? DriverName { get; set; }
    public DateTime ReceivedDate { get; set; }
    public string WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public string ReceivedFrom { get; set; } //supplier or donor
    public List<StockDetail> StockDetailList { get; set; }
}

public class StockDetail : CommonModel 
{
	public string ItemId { get; set; }
	public StockItem Item { get; set; }
    public int Quantity { get; set; }
	public string RackId { get; set; }
    public Rack Rack { get; set; }
	public DateTime? ExpiryDate { get; set; }
    public string BatchNo { get; set; }
    public ReliefReceived BatchDetail { get; set; }
}

public class Floor : CommonModel 
{
    public string Name { get; set; }
    public string WarehouseId { get; set; }
	public Warehouse Warehouse { get; set; }
    public List<Rack> Racks { get; set; }
}
