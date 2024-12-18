
using LAKAPSAGAP.Models.Models;
using Mapster;

namespace LAKAPSAGAP.BlazorServer.Controls.Dashboard;

public partial class DashboardControl
{
	[Inject] IDashboardRepository _dashboardRepo { get; set; }

	List<PendingReliefRequestVM> _pendingReliefRequestsAll { get; set; }
	List<PendingReliefRequestVM> _pendingReliefRequestsToday { get; set; }
	List<ReleasedOfReliefGoodsVM> _releasedOfReliefGoodsPerMonth { get; set; }
	List<ReceivedDonationsByBarangayVM> _receivedDonationsByBarangayList { get; set; }
	List<KitStatusVM> _kitStatusList { get; set; }
	List<StocksInWarehouseVM> _stocksInWarehouses { get; set; }

	int _receivedStocksCount { get; set; }
	int _selectedYear { get; set; }

	protected override void OnInitialized()
	{
		_pendingReliefRequestsAll ??= [];
		_pendingReliefRequestsToday ??= [];
		_receivedDonationsByBarangayList ??= [];
		_kitStatusList ??= [];
		_stocksInWarehouses ??= [];
		_selectedYear = DateTime.Today.Year;

		_releasedOfReliefGoodsPerMonth ??= Enumerable.Range(1, 12)
			  .Select(month => new ReleasedOfReliefGoodsVM
			  {
				  ReleasedDate = new DateOnly(_selectedYear, month, 1),
				  Total = 0
			  }).ToList();
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if(firstRender)
		{
			await LoadStocksInWarehouses();
			await LoadAllPendingReliefRequests();
			await LoadKitStatuses();
			await LoadAllReceivedDonationsByBarangay();
			await LoadReceivedStocksCount();
			await LoadReleasedOfReliefGoods();
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task LoadStocksInWarehouses()
	{
		IReadOnlyList<StocksInWarehouse> stocksInWhses = await _dashboardRepo.GetAllStocksInWarehouses();
		_stocksInWarehouses = stocksInWhses.Adapt<List<StocksInWarehouseVM>>();
	}

	async Task LoadAllPendingReliefRequests()
	{
		IReadOnlyList<PendingReliefRequest> pendingReliefRequests = await _dashboardRepo.GetAllPendingReliefRequests();
		_pendingReliefRequestsAll = pendingReliefRequests.Adapt<List<PendingReliefRequestVM>>();
		LoadPendingReliefRequestsToday();
	}

	void LoadPendingReliefRequestsToday()
	{
		_pendingReliefRequestsToday = _pendingReliefRequestsAll.Where(x => DateOnly.FromDateTime(x.RequestDate) == DateOnly.FromDateTime(DateTime.Today)).ToList();
	}

	async Task LoadAllReceivedDonationsByBarangay()
	{
		IReadOnlyList<ReceivedDonationsByBarangay> rcvdDonationsByBarangays = await _dashboardRepo.GetAllReceivedDonationsByBarangays();
		_receivedDonationsByBarangayList = rcvdDonationsByBarangays.Adapt<List<ReceivedDonationsByBarangayVM>>();
	}

	async Task LoadKitStatuses()
	{
		IReadOnlyList<KitStatus> kitStatuses = await _dashboardRepo.GetAllKitStatuses();
		_kitStatusList = kitStatuses.Adapt<List<KitStatusVM>>();
	}

	async Task LoadReceivedStocksCount()
	{
		_receivedStocksCount = await _dashboardRepo.GetReceivedStocksCount();
	}

	async Task LoadReleasedOfReliefGoods()
	{
		IReadOnlyList<ReleasedOfReliefGoods> releasedOfReliefGoods = await _dashboardRepo.GetAllReleasedOfReliefGoods(_selectedYear);
		if (releasedOfReliefGoods.Count > 0)
			releasedOfReliefGoods
				.GroupBy(x => x.ReleasedDate.Month)
				.Select(group => new ReleasedOfReliefGoodsVM()
				{
					ReleasedDate = DateOnly.FromDateTime(new DateTime(_selectedYear, group.Key, 1)),
					Total = group.Sum(rorg => rorg.Total)
				})
				.ToList()
				.ForEach(rorg =>
				{
					_releasedOfReliefGoodsPerMonth.First(x => x.ReleasedDate == rorg.ReleasedDate).Total = rorg.Total;
				});
	}

	async Task LoadReleasedOfReliefGoodsWrapper()
	{
		await LoadReleasedOfReliefGoods();
		StateHasChanged();
	}

	string FormatAsMonthsName(object value) => value is not null ? ((DateOnly)value).ToString("MMM") : string.Empty;

	#region Classes
	class PendingReliefRequestVM
	{
		public string RequestorName { get; set; }
		public string? RequestorEmail { get; set; }
		public string Image { get; set; }
		public string Barangay { get; set; }
		public string KitName { get; set; }
		public string MainReason { get; set; }
		public DateTime RequestDate { get; set; }
	}

	class ReleasedOfReliefGoodsVM
	{
		public DateOnly ReleasedDate { get; set; }
		public double Total { get; set; }
	}

	class ReceivedDonationsByBarangayVM
	{
		public string BarangayName { get; set; }
		public int Total { get; set; }
	}
	
	class KitStatusVM
	{
		public string KitName { get; set; }
		public int Total { get; set; }
	}

	class StocksInWarehouseVM
	{
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public double Total { get; set; }
    }
	#endregion Classes
}