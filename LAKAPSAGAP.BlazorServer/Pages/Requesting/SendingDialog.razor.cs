using System;
namespace LAKAPSAGAP.BlazorServer.Pages.Requesting
{
	public partial class SendingDialog
	{
		[Inject] IJSRuntime _jSRuntime { get; set; }
		[Inject] DialogService _dialogService { get; set; }
        [Inject] IReliefRequestRepository _requestRepo { get; set; }
		[Parameter]
		public string RequestDetailsId { get; set; }
        [Parameter]
		public List<RequestViewModel> RequestItemsList { get; set; }

		public List<RequestViewModel> SentItemList { get; set; } = new();
		List<KitViewModel> KitListVM = new List<KitViewModel>();
		List<StockItemViewModel> StockItemListVM = new List<StockItemViewModel>();
		SentItemForm ItemForm { get; set; } = new();

		RadzenDataGrid<RequestViewModel> sentItemListDG = new();

		bool _isBusy { get; set; } = false;
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await GetItemsAndKits();
				StateHasChanged();
			}

			//await base.OnAfterRenderAsync(firstRender);
		}

		public class SentItemForm
		{
			public RequestType RequestType { get; set; }
			public string UnitId { get; set; }
			public int Quantity { get; set; }
			public string SearchString { get; set; }
		}
		async Task GetItemsAndKits()
		{
			KitListVM = (await _requestRepo.GetAllKitAsync()).Select(x => new KitViewModel
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();

			StockItemListVM = (await _requestRepo.GetAllStockItemAsync()).Select(x => new StockItemViewModel
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();
		}
		async Task AddToSent(RequestViewModel request)
		{

			if (SentItemList.Contains(request))
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Item already added to sent list.");
				return;
			}
			SentItemList.Add(request);
			await sentItemListDG.Reload();
			StateHasChanged();
		}

		async Task AddToSentCustom(SentItemForm request)
		{
			RequestViewModel item = new RequestViewModel
			{
				RequestType = request.RequestType,
				UnitId = request.UnitId,
				Quantity = request.Quantity,

			};
		}

		public async Task Submit()
		{
            if (!(await _jSRuntime.InvokeAsync<bool>("Confirmation"))) return;
			_isBusy = true;
			StateHasChanged();
            ReliefSentViewModel reliefSentVM = new ReliefSentViewModel
            {
                ReliefRequestId = RequestDetailsId,
                SentKitList = KitListVM.Select(x => new ReliefSentKitViewModel
                {
                    KitId = x.Id,
                    Quantity = 0
                }).ToList(),
                SentItemList = StockItemListVM.Select(x => new ReliefSentItemViewModel
                {
                    StockItemId = x.Id,
                    Quantity = 0
                }).ToList()
            };
			await _requestRepo.SendRelief(reliefSentVM);
			_isBusy = false;
            StateHasChanged();
			_dialogService.Close("sent");
        }
	}
}