

using static LAKAPSAGAP.Models.ViewModel.ReliefReceivedViewModel;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
    public partial class StocksDataGrid
    {
        [Inject] IStockDetailsRepository _stockDetailsRepo {get; set;}
        [Inject] IStockItemRepository _stockItemRepo {get; set;}
        [Parameter] public EventCallback<bool> OnValueChanged { get; set; }
        [Parameter] public bool ReloadKitsTable { get; set; } = false;

        //
        // On Initialized Async Adapt the Data Service Model into _tableData
        //
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadStocksData();
                StateHasChanged();
            }
        }
        private List<StockDetailViewModel> StockDetailList { get; set; } = new();
        private List<StockItem> _tableData { get; set; } = new();
        private List<StockItem> _stockItems { get; set; } = new();
        List<StockItemViewModel> _stockItemsVM { get; set; } = new();
        async Task LoadStocksData()
        {
            //List<StockDetail> _stocksDetails = await _stockDetailsRepo.GetAllStockDetailsActive();
            //_tableData = _stocksDetails.Select(x => new StockDetailViewModel
            //{
            //    Id = x.Id,
            //    BatchNumber = x.BatchNo,
            //    Item = new StockItemViewModel
            //    {
            //        Id = x.Item.Id,
            //        Name = x.Item.Name,
            //        CategoryName = x.Item.Category.Name,
            //        UoMName = x.Item.UoM.Name,
            //    },
            //    Quantity = x.Quantity,
            //}).ToList();
            _stockItemsVM = await _stockItemRepo.GetStocks();
            _tableData = _stockItems;
            StateHasChanged();

		}

        protected override async Task OnParametersSetAsync()
        {
            if (ReloadKitsTable)
            {
                await LoadStocksData();
                ChangeValue(false);
            }
        }

        void ChangeValue(bool ReloadTable)
        {
            OnValueChanged.InvokeAsync(ReloadTable);
        }

        //private void SearchStockDetails(string value)
        //{
        //    if (value != string.Empty)
        //    {
        //        _tableData = StockDetailList.Where(x => x.Id.ToString().ToLower().Contains(value.ToLower())
        //           || x.BatchNumber.ToString().ToLower().Contains(value.ToLower())
        //           || x.ItemId.ToString().ToLower().Contains(value.ToLower())
        //           || x.TypeId.ToString().ToLower().Contains(value.ToLower())
        //           || x.CategoryId.ToString().ToLower().Contains(value.ToLower())
        //           || x.Quantity.ToString().ToLower().Contains(value.ToLower())
        //  || x.UoMId.ToString().ToLower().Contains(value.ToLower())
        //           || x.RackId.ToString().ToLower().Contains(value.ToLower())
        //  || x.ExpiryDate.ToString().ToLower().Contains(value.ToLower())).ToList();
        //    }
        //    else
        //    {
        //        _tableData = StockDetailList;
        //    }
        //}
    }
}
