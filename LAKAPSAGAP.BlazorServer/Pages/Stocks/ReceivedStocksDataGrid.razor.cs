
using LAKAPSAGAP.Models.ViewModel;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
	public partial class ReceivedStocksDataGrid
	{

		//
		// On Initialized Async Adapt the Data Service Model into _tableData
		//


		private List<ReliefReceivedViewModel> ReceivedStocks { get; set; } = new();
		private List<ReliefReceivedViewModel> _tableData { get; set; } = new();

		private void SearchStockDetails(string value)
		{
			if (value != string.Empty)
			{
				_tableData = ReceivedStocks.Where(
					x => x.Id.ToString().ToLower().Contains(value.ToLower())
						|| x.Id.ToString().ToLower().Contains(value.ToLower())
						|| x.ReliefType.ToString().ToLower().Contains(value.ToLower())
						|| x.ReceivedFrom.ToString().ToLower().Contains(value.ToLower())
						|| x.ReceivedDate.ToString().ToLower().Contains(value.ToLower())
						|| x.Warehouse.Name.ToString().ToLower().Contains(value.ToLower())
					).ToList();
			}
			else
			{
				_tableData = ReceivedStocks;
			}
		}
	}
}
