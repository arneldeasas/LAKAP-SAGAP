
using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.Models.ViewModel
{
    public class ReliefReceivedViewModel
    {

		public string Id { get; set; }
		public string WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = new();
		public string ReliefType { get; set; }
		public string ReceivedBy { get; set; }
		public string ReceivedFrom { get; set; }
		public string? TruckPlateNumber { get; set; }
		public string? DriverName { get; set; }
		public DateTime ReceivedDate { get; set; }
		public int UserId { get; set; }
		public UserInfo AddedBy { get; set; }
		public List<StockDetailViewModel> StockDetailViewList { get; set; }

		public ReliefReceivedFormSelections ReliefReceivedFormSelections { get; set; } // contains all records for selection type input

    }
	public class ReliefReceivedFormSelections
	{
		public List<StockType> StockTypeList { get; set; }
		public List<StockCategory> StockCategoryList { get; set; }
		public List<StockItem> StockItemList { get; set; } 
		public List<UoM> UoMList { get; set; }
		public List<Floor> FloorList { get; set; }
		public List<Rack> RackList { get; set; }
	}
	public class StockDetailViewModel
	{
		public string Id { get; set; }
		public string BatchNumber { get; set; }
		public string TypeId { get; set; }
		public StockType Type { get; set; }
		public string ItemName { get; set; }
		public string CategoryId { get; set; }
		public StockCategory Category { get; set; }
		public int Quantity { get; set; }
		public string UnitOfMeasure { get; set; }
		public string Floor { get; set; }
		public string Rack { get; set; }
		public DateTime? ExpiryDate { get; set; }
		
		public ReliefReceivedViewModel BatchDetail { get; set; }
	}
}
