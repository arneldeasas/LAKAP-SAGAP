
using LAKAPSAGAP.Models.ViewModel;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
	public partial class ReceivedStocksDataGrid
	{
		[Inject] IReliefReceivedRepository _reliefReceivedRepo { get; set; }
		//
		// On Initialized Async Adapt the Data Service Model into _tableData
		//


		private List<ReliefReceivedViewModel> ReceivedStocks { get; set; } = new();
		public List<ReliefReceivedViewModel> TableData { get; set; } = new();

		[Parameter]public bool ReloadTable { get; set; }

		protected override async Task OnInitializedAsync()
		{

			await LoadBatchesData(); // To be enhanced;
		}
		protected override async Task OnParametersSetAsync()
		{
			if (ReloadTable)
			{
				await LoadBatchesData();
				StateHasChanged();
			}
		}
		async Task LoadBatchesData()
		{
			List<ReliefReceived> _batches = await _reliefReceivedRepo.GetAllBatches();
			TableData = _batches.Select(x => new ReliefReceivedViewModel
			{
				Id = x.Id,
				ReceivedFrom = x.ReceivedFrom,
				ReceivedDate = x.ReceivedDate,
				Warehouse = x.Warehouse,
				ReceivedBy = x.ReceivedBy,
				DriverName = x.DriverName,
				StockDetailViewList = x.StockDetailList.Select(x=>new StockDetailViewModel
				{
					Id = x.Id,
					BatchNumber = x.BatchNo,
					Item = new StockItemViewModel
					{
						Name = x.Item.Name,
						UoMName = x.Item.UoM.Name,
					},
					Quantity = x.Quantity,
					
				}).ToList()
			}).ToList();
		}
		private void SearchStockDetails(string value)
		{
			if (value != string.Empty)
			{
						//|| x.ReliefType.ToString().ToLower().Contains(value.ToLower())
				TableData = ReceivedStocks.Where(
					x => x.Id.ToString().ToLower().Contains(value.ToLower())
						|| x.Id.ToString().ToLower().Contains(value.ToLower())
						|| x.ReceivedFrom.ToString().ToLower().Contains(value.ToLower())
						|| x.ReceivedDate.ToString().ToLower().Contains(value.ToLower())
						|| x.Warehouse.Name.ToString().ToLower().Contains(value.ToLower())
					).ToList();
			}
			else
			{
				TableData = ReceivedStocks;
			}
		}
	}
}
