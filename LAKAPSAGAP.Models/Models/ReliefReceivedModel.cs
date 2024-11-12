
namespace LAKAPSAGAP.Models.Models
{
    [Table("ReliefReceived")]
    public class ReliefReceived : CommonModel
    {
       
        public string WarehouseId { get; set; }
        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }
        public string ReliefType { get; set; }
        public string ReceivedBy { get; set; }
        public string ReceivedFrom { get; set; }
        public string? TruckPlateNumber { get; set; }
        public string? DriverName { get; set; }
        public DateTime ReceivedDate { get; set; }
        public List<StockDetail> StockDetailList { get; set; }
    }

    [Table("StockDetail")]
    public class StockDetail : CommonModel
    {
      
        public string BatchNumber { get; set; }  
		public string ItemId { get; set; }
		[ForeignKey(nameof(ItemId))]
		public StockItem Item { get; set; }
	
		public int Quantity { get; set; }
		public string FloorId { get; set; }
        [ForeignKey(nameof(FloorId))]
        public Floor Floor { get; set; }                                  
		public string RackId { get; set; }
		[ForeignKey(nameof(RackId))]
        public Rack Rack { get; set; }
		public DateTime? ExpiryDate { get; set; }
        [ForeignKey(nameof(BatchNumber))]
        public ReliefReceived BatchDetail { get; set; }

    }

    [Table("StockType")]
    public class StockType : CommonModel
    {
        
        public string Name { get; set; }
        
    }

    [Table("StockCategory")]
    public class StockCategory : CommonModel
    {
        
        public string Name { get; set; }

    }

    [Table("StockItem")]
    public class StockItem : CommonModel 
    {
      
        //public string StockTypeId { get; set; }
        //[ForeignKey(nameof(StockTypeId))]
        //public StockType StockType { get; set; }
       
        public string StockCategoryId { get; set; }
        [ForeignKey(nameof(StockCategoryId))]
        public StockCategory StockCategory { get; set; }
        public string UoMId { get; set; }
        [ForeignKey(nameof(UoMId))]
        public UoM UoM { get; set; }
        public string Name { get; set; }


    }

    public class UoM : CommonModel
    {
        public string Name { get; set; }
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
}
