namespace LAKAPSAGAP.BlazorServer.Pages.Requesting
{
	public partial class RequestingForm
	{
		[Inject] IReliefRequestRepository ReliefRequestRepository { get; set; }

		//Form Initializations
		List<string> BarangayList { get; set; } = new();
		List<Kit> KitList { get; set; } = new();
		List<StockItem> StockItemList { get; set; } = new();

		//Form State
		ReliefRequestDetailViewModel ReliefRequestVM { get; set; } = new();
		UnitFormViewModel KitVM { get; set; } = new ();
		UnitFormViewModel ItemVM { get; set; } = new ();
		protected override async Task OnInitializedAsync()
		{
			Task<List<string>> BarangayListTask = ReliefRequestRepository.GetAllBarangayAsync();

			KitList = await ReliefRequestRepository.GetAllKitAsync();
			StockItemList = await ReliefRequestRepository.GetAllStockItemAsync();
			BarangayList = await BarangayListTask;
		}

		void AddItem(UnitFormViewModel unitVM)
		{
			ReliefRequestVM.RequestList.Add(new RequestViewModel
			{
				RequestType = RequestType.Item,
				UnitId = unitVM.UnitId,
				Quantity = unitVM.Quantity,
				UnitName = StockItemList.FirstOrDefault(x => x.Id == unitVM.UnitId)?.Name ?? ""

			});
			unitVM.resetForm();
		}
		void AddKit(UnitFormViewModel unitVM)
		{

			ReliefRequestVM.RequestList.Add(new RequestViewModel
			{
				RequestType = RequestType.Kit,
				UnitId = unitVM.UnitId,
				Quantity = unitVM.Quantity,
				UnitName = KitList.FirstOrDefault(x => x.Id == unitVM.UnitId)?.Name ?? ""
			});
			unitVM.resetForm();
		}

	}

	public class UnitFormViewModel
	{
		public string UnitId { get; set; }
		public string SearchString { get; set; }
		public int Quantity { get; set; }

		public void resetForm()
		{
			UnitId = "";
			Quantity = 0;
		}
	}
}