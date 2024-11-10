
using static LAKAPSAGAP.Models.ViewModel.ReliefReceivedViewModel;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
	public partial class ReceiveStockForm
	{
		[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
		private IJSObjectReference _js { get; set; } = default!;
		//
		// On Initialized Async Adapt the Data Service Model into _tableData
		//
		private List<StockDetailViewModel> StockDetailList { get; set; } = new();
		private List<StockDetailViewModel> _tableData { get; set; } = new();
		private ReliefReceivedViewModel headers { get; set; } = new();
		private string _activePane { get; set; } = "form";

		


		private async Task ConfirmReceiving()
		{

			bool confirmed = await _jSRuntime.InvokeAsync<bool>("Confirmation");

			//if (confirmed)
			//{
			//	// logic to add the batch
			//}

		}

		public void changeTab(string tab)
		{
			_activePane = tab;
		}
	}
}
