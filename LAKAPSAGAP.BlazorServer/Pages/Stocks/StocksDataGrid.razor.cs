

using static LAKAPSAGAP.Models.ViewModel.ReliefReceivedViewModel;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
    public partial class StocksDataGrid
    {
		//
		// On Initialized Async Adapt the Data Service Model into _tableData
		//
		private List<StockDetailViewModel> StockDetailList { get; set; } = new();
        private List<StockDetailViewModel> _tableData { get; set; } = new();

        private void SearchStockDetails(string value)
        {
            if (value != string.Empty)
            {
                _tableData = StockDetailList.Where( x => x.Id.ToString().ToLower().Contains(value.ToLower())
                   || x.BatchNumber.ToString().ToLower().Contains(value.ToLower())
                   || x.ItemId.ToString().ToLower().Contains(value.ToLower())
                   || x.TypeId.ToString().ToLower().Contains(value.ToLower())
                   || x.CategoryId.ToString().ToLower().Contains(value.ToLower())
                   || x.Quantity.ToString().ToLower().Contains(value.ToLower())
				   || x.UoMId.ToString().ToLower().Contains(value.ToLower())
                   || x.RackId.ToString().ToLower().Contains(value.ToLower())
				   || x.ExpiryDate.ToString().ToLower().Contains(value.ToLower())).ToList();
			}
            else
            {
                _tableData = StockDetailList; 
            }
        }
    }
}
