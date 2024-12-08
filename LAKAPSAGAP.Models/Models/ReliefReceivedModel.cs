using LAKAPSAGAP.Models.Enums;

namespace LAKAPSAGAP.Models.Models;

// GRPO => Relief Received (Batch)
public class ReliefReceived : CommonModel
{
	public AcquisitionTypes AcquisitionType { get; set; }
	public string ReceivedBy { get; set; }

	// Delivery Details
	public string PlateNo { get; set; }
	public string DriverName { get; set; }
	public string? ReceivedFrom { get; set; }
	public DateTime ReceivedDate { get; set; } = DateTime.Now;

	public string WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    public List<StockDetail> StockDetailList { get; set; }
}

public class StockDetail : CommonModel 
{
	public string BatchNo { get; set; }
	public string StockItemId { get; set; }
	public string CategoryId { get; set; }
	public string UomId { get; set; }
    public int Quantity { get; set; }
	public string FloorId { get; set; }
	public string RackId { get; set; }
	public DateTime? ExpiryDate { get; set; }

	public ReliefReceived BatchDetail { get; set; }
	public StockItem Item { get; set; }
    public Rack Rack { get; set; }
}

public class Floor : CommonModel 
{
    public string Name { get; set; }
    public string WarehouseId { get; set; }
	public Warehouse Warehouse { get; set; }
    public List<Rack> Racks { get; set; }
    public List<PackedReliefKit> PackedReliefKitList { get; set; }
}
