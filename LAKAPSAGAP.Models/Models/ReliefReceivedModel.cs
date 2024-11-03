
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
        DateTime ReceivedDate { get; set; }
        public List<StockDetail> StockDetailList { get; set; }
    }

    [Table("StockDetail")]
    public class StockDetail : CommonModel
    {
      
        public string BatchNumber { get; set; }
        public string Type { get; set; } //Item Type
        public string ItemName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Floor { get; set; }
        public string Rack { get; set; }
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
      
        public string StockTypeId { get; set; }
        [ForeignKey(nameof(StockTypeId))]
        public StockType StockType { get; set; }
       
        public string StockCategoryId { get; set; }
        [ForeignKey(nameof(StockCategoryId))]
        public StockCategory StockCategory { get; set; }
        public string Name { get; set; }


    }

    public class Floor : CommonModel 
    {
      
        public string Name { get; set; }
        public List<Rack> Racks {get; set;}

    }

    public class Rack : CommonModel
    {
    
        string Name { get; set; }
        string FloorId { get; set; }
        [ForeignKey(nameof(FloorId))]
        public Floor Floor { get; set; }

    }
}
