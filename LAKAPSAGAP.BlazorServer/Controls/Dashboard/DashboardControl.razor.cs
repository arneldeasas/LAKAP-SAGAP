namespace LAKAPSAGAP.BlazorServer.Controls.Dashboard;

public partial class DashboardControl
{
	List<PendingReliefRequest> _pendingReliefRequestsToday { get; set; }
	List<ReleasedOfReliefGoods> _releasedOfReliefGoodsPerMonth { get; set; }
	List<ReceivedDonationsByBarangay> _receivedDonationsByBarangayList { get; set; }
	List<StocksInWarehouseVM> _stocksInWarehouses { get; set; }

	int _selectedYear { get; set; }

	protected override void OnInitialized()
	{
		_pendingReliefRequestsToday ??= [
			new(){
				RequestorName = "Kap. Eduardo Jr.",
				RequestorEmail = "arneldeasas@gmail.com",
				Barangay = "Karuhatan",
				KitName = "Food Pack",
				MainReason = "Fire Incident",
				RequestDate = DateTime.Now,
			},
			new(){
				RequestorName = "CSWD Admin Ernesto",
				RequestorEmail = "naian@gmail.com",
				Barangay = "Dalandanan",
				KitName = "Family Kit",
				MainReason = "Flood Incident",
				RequestDate = DateTime.Now,
			},
			new(){
				RequestorName = "CSWD Admin Soledad",
				RequestorEmail = "charlesm.herrera0700@gmail.com",
				Barangay = "Manotoc",
				KitName = "Food Pack",
				MainReason = "Bahay Kalinga",
				RequestDate = DateTime.Now,
			}];

		_releasedOfReliefGoodsPerMonth ??= [
			new(){
				ReleasedDate = new DateTime(2023, 1, 1),
				Total = 69,
			},
			new(){
				ReleasedDate = new DateTime(2023, 2, 1),
				Total = 32,
			},
			new(){
				ReleasedDate = new DateTime(2023, 3, 1),
				Total = 109,
			},
			new(){
				ReleasedDate = new DateTime(2023, 4, 1),
				Total = 37,
			},
			new(){
				ReleasedDate = new DateTime(2023, 5, 1),
				Total = 45,
			},
			new(){
				ReleasedDate = new DateTime(2023, 6, 1),
				Total = 91,
			},
			new(){
				ReleasedDate = new DateTime(2023, 7, 1),
				Total = 60,
			},
			new(){
				ReleasedDate = new DateTime(2023, 8, 1),
				Total = 116,
			},
			new(){
				ReleasedDate = new DateTime(2023, 9, 1),
				Total = 73,
			},
			new(){
				ReleasedDate = new DateTime(2023, 10, 1),
				Total = 67,
			},
			new(){
				ReleasedDate = new DateTime(2023, 11, 1),
				Total = 82,
			},
			new(){
				ReleasedDate = new DateTime(2023, 12, 1),
				Total = 62,
			},
			new(){
				ReleasedDate = new DateTime(2024, 1, 1),
				Total = 62,
			}];

		_receivedDonationsByBarangayList ??= [
			new(){
				BarangayName="Arkong Bato",
				Total=17
			},
			new(){
				BarangayName="Bagbaguin",
				Total=42
			},
			new(){
				BarangayName="Balangkas",
				Total=21
			},
			new(){
				BarangayName="Bignay",
				Total=13
			},
			new(){
				BarangayName="Bisig",
				Total=27
			},
			new(){
				BarangayName="Canumay East",
				Total=37
			},
			new(){
				BarangayName="Coloong",
				Total=34
			},
			new(){
				BarangayName="Dalandanan",
				Total=22
			},
			new(){
				BarangayName="Gen T. De Leon",
				Total=10
			},
			new(){
				BarangayName="Isla",
				Total=6
			},
			new(){
				BarangayName="Karuhatan",
				Total=107
			},
			new(){
				BarangayName="Lawang Bato",
				Total=29
			},
			new(){
				BarangayName="Malinta",
				Total=21
			}];

		_stocksInWarehouses ??= [
			new() {
				WarehouseId = "WHS_001",
				WarehouseName = "Alert",
				Total = 120
			},
			new() {
				WarehouseId = "WHS_002",
				WarehouseName = "Malinta Barangay Hall",
				Total = 560
			},
			new() {
				WarehouseId = "WHS_003",
				WarehouseName = "PLV Stock Room",
				Total = 729
			}
			];

		_selectedYear = _releasedOfReliefGoodsPerMonth.DistinctBy(x => x.ReleasedDate.Year).First().ReleasedDate.Year;
		base.OnInitialized();
	}

	string FormatAsMonthsName(object value)
	{
		return ((DateTime)value).ToString("MMM");
	}

	class PendingReliefRequest
	{
		public string RequestorName { get; set; }
		public string? RequestorEmail { get; set; }
		public string Image { get; set; }
		public string Barangay { get; set; }
		public string KitName { get; set; }
		public string MainReason { get; set; }
		public DateTime RequestDate { get; set; }
	}

	class ReleasedOfReliefGoods
	{
		public DateTime ReleasedDate { get; set; }
		public double Total { get; set; }
	}

	class ReceivedDonationsByBarangay
	{
		public string BarangayName { get; set; }
		public int Total { get; set; }
	}

	class StocksInWarehouseVM
	{
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public double Total { get; set; }
    }
}