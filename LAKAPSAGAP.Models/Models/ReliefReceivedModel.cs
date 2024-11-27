namespace LAKAPSAGAP.Models.Models;

// GRPO => Relief Received (Batch)
public class ReliefReceived : CommonModel
{
   
    public string WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public string ReliefType { get; set; } // Acquisition Type (kung purchase or donation)
    public string ReceivedBy { get; set; }
    public string ReceivedFrom { get; set; } //supplier or donor
	public string? TruckPlateNumber { get; set; }
    public string? DriverName { get; set; }
    public DateTime DateReceived { get; set; }

    // Document Line (Items within the Relief Received or in a Batch)
    public List<StockDetail> StockDetailList { get; set; }
}

public class StockDetail : CommonModel 
{
	public string ItemId { get; set; }
	public StockItem Item { get; set; }
    public int Quantity { get; set; }
	public string RackId { get; set; }
    public Rack Rack { get; set; }
	public DateTime? DateExpiry { get; set; }
    public string BatchNumber { get; set; }
    public ReliefReceived BatchDetail { get; set; }
}

public class Floor : CommonModel 
{
    public string Name { get; set; }
    public string WarehouseId { get; set; }
	public Warehouse Warehouse { get; set; }
    public List<Rack> Racks { get; set; }
    public List<StockItem> StockItems { get; set; }
}
