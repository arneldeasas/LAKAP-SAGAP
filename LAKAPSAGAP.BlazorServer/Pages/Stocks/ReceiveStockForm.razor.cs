
using static LAKAPSAGAP.Models.ViewModel.ReliefReceivedViewModel;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
	public partial class ReceiveStockForm
	{
		//
		// On Initialized Async Adapt the Data Service Model into _tableData
		//
		private List<StockDetailViewModel> StockDetailList { get; set; } = new();
		private List<StockDetailViewModel> _tableData { get; set; } = new();
	}
}
