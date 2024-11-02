using static LAKAPSAGAP.Models.ViewModels.ReliefReceivedViewModel;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
	public partial class ReceiveStockForm
	{
		//
		// On Initialized Async Adapt the Data Service Model into _tableData
		//
		private List<StockDetail> StockDetailList { get; set; } = new();
		private List<StockDetail> _tableData { get; set; } = new();
	}
}
