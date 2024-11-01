
namespace LAKAPSAGAP.Models.Models
{
    [Table("ReliefReceived")]
    public class ReliefReceived
    {
        [Key]
        public string Id { get; set; }//Batch Number
        public string WarehouseId { get; set; }
        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }
        public string ReliefType { get; set; }
        public string ReceivedBy { get; set; }
        public string ReceivedFrom { get; set; }
        public string? TruckPlateNumber { get; set; }
        public string? DriverName { get; set; }
        DateTime ReceivedDate { get; set; }
        public int UserId { get; set; } //userid of the user who added the relief
        [ForeignKey(nameof(UserId))]
        public UserInfo AddedBy { get; set; } 
        public ICollection<StockDetail> StockDetailList { get; set; }
    }

    [Table("StockDetail")]
    public class StockDetail
    {
        [Key]
        public string Id { get; set; } //Item Code
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
    public class StockType
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; } //userid of the user who added the stock type
        [ForeignKey(nameof(UserId))]
        public UserInfo AddedBy { get; set; }
    }

    [Table("StockCategory")]
    public class StockCategory
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; } //userid of the user who added the stock category
        [ForeignKey(nameof(UserId))]
        public UserInfo AddedBy { get; set; }
    }

    [Table("StockItem")]
    public class StockItem
    {
        [Key]
        public string Id { get; set; }
        public string StockTypeId { get; set; }
        public string StockCategoryId { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(StockTypeId))]
        public StockType Type { get; set; }
        [ForeignKey(nameof(StockCategoryId))]
        public StockCategory Category { get; set; }
        public int UserId { get; set; } //userid of the user who added the stock item
        [ForeignKey(nameof(UserId))]
        public UserInfo AddedBy { get; set; }
    }

    public class Floor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Rack> Racks {get; set;}
        public int UserId { get; set; } //userid of the user who added the Floor
        [ForeignKey(nameof(UserId))]
        public UserInfo AddedBy { get; set; }
    }

    public class Rack
    {
        string Id { get; set; }
        string Name { get; set; }
        string FloorId { get; set; }
        [ForeignKey(nameof(FloorId))]
        public Floor Floor { get; set; }
        public int UserId { get; set; } //userid of the user who added the Rack
        [ForeignKey(nameof(UserId))]
        public UserInfo AddedBy { get; set; }
    }
}
