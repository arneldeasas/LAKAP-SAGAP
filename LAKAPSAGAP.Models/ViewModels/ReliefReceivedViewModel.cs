using LAKAPSAGAP.Models.Models;
using static LAKAPSAGAP.Models.ViewModel.WarehouseViewModel;
using static LAKAPSAGAP.Models.ViewModel.UserInfoViewModel;

namespace LAKAPSAGAP.Models.ViewModel
{
    public class ReliefReceivedViewModel
    {

		public string Id { get; set; }
		public string WarehouseId { get; set; }
		public Warehouse Warehouse { get; set; }
		public string ReliefType { get; set; }
		public string ReceivedBy { get; set; }
		public string ReceivedFrom { get; set; }
		public string? TruckPlateNumber { get; set; }
		public string? DriverName { get; set; }
		public DateTime ReceivedDate { get; set; }
		public int UserId { get; set; }
		public UserInfoViewModel AddedBy { get; set; }
		public ICollection<StockDetailViewModel> StockDetailList { get; set; }

		public class StockDetailViewModel
        {
            public string Id { get; set; }
            public string BatchNumber { get; set; }
            public string Type { get; set; }
            public string ItemName { get; set; }
            public string Category { get; set; }
            public int Quantity { get; set; }
            public string UnitOfMeasure { get; set; }
            public string Floor { get; set; }
            public string Rack { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public ReliefReceivedViewModel BatchDetail { get; set; }
        }

    }
}
