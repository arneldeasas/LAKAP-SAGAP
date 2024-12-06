using LAKAPSAGAP.BlazorServer.Pages.UserManagement;
using Radzen;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
	public partial class Stocks
	{
		[Parameter] public string? id { get; set; }
		[Inject] DialogService _dialogService { get; set; }
		[Inject] IReliefReceivedRepository _reliefReceivedRepo { get; set; }
		private List<BreadcrumbViewModel> Breadcrumbs = new()
		{
			new BreadcrumbViewModel { Path = "/Warehouse", Text = "Warehouse" },
		};

		List<ReliefReceivedViewModel> BatchesTable = new();

	
		async Task LoadBatchesData()
		{
			List<ReliefReceived> _batches = await _reliefReceivedRepo.GetAllReliefReceived();
			BatchesTable = _batches.Select(x => new ReliefReceivedViewModel
			{
				Id = x.Id,
				ReceivedFrom = x.ReceivedFrom,
				ReceivedDate = x.ReceivedDate,
				Warehouse = x.Warehouse,
				ReceivedBy = x.ReceivedBy,
				DriverName = x.DriverName
			}).ToList();
		}
	}
}
