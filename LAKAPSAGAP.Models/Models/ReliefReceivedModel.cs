namespace LAKAPSAGAP.Models.Models;

[Table("ReliefReceived")]
public class ReliefReceived : CommonModel
{
   
    public string WarehouseId { get; set; }
    [ForeignKey(nameof(WarehouseId))]
    public Warehouse Warehouse { get; set; }
    public string ReliefType { get; set; }
    public string ReceivedBy { get; set; }
    public string ReceivedFrom { get; set; } //supplier or donor
	public string? TruckPlateNumber { get; set; }
    public string? DriverName { get; set; }
    public DateTime DateReceived { get; set; }
    public List<StockDetail> StockDetailList { get; set; }
}

[Table("StockDetail")]
public class StockDetail : CommonModel 
{
	public string ItemId { get; set; }

	[ForeignKey(nameof(ItemId))]
	public StockItem Item { get; set; }

	public string CategoryId { get; set; }

	[ForeignKey(nameof(CategoryId))]
	public StockCategory Category { get; set; }
    
    public int Quantity { get; set; }

    public string UoMId { get; set; }

	[ForeignKey(nameof(UoMId))]
	public UoM UoM { get; set; }
    
	public string RackId { get; set; }

	[ForeignKey(nameof(RackId))]
    public Rack Rack { get; set; }

	public DateTime? DateExpiry { get; set; }

    public string BatchNumber { get; set; }

    [ForeignKey(nameof(BatchNumber))]
    public ReliefReceived BatchDetail { get; set; }

}

[Table("StockCategory")]
public class StockCategory : CommonModel //Food, Hygiene, Kitchen, clothes etc
{
    public string Name { get; set; }
}

[Table("StockItem")] 
public class StockItem : CommonModel //record of all Relief Items
{
    public string StockCategoryId { get; set; }

    [ForeignKey(nameof(StockCategoryId))]
    public StockCategory StockCategory { get; set; }

    public string Name { get; set; }

    public string UoMId { get; set; }

    [ForeignKey(nameof(UoMId))]
	public UoM UoM { get; set; }
}

public class Floor : CommonModel 
{
    public string Name { get; set; }

    public string WarehouseId { get; set; }

	[ForeignKey(nameof(WarehouseId))]
	public Warehouse Warehouse { get; set; }

    public List<Rack> RackList { get; set; } = new();
}

public class Rack : CommonModel
{
    public string Name { get; set; }

    public string FloorId { get; set; }

    [ForeignKey(nameof(FloorId))]
    public Floor Floor { get; set; }
}
