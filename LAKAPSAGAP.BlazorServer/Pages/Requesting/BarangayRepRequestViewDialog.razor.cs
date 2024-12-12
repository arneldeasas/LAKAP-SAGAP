namespace LAKAPSAGAP.BlazorServer.Pages.Requesting
{
	public partial class BarangayRepRequestViewDialog
	{
	
		[Inject] DialogService _dialogService { get; set; }

		[Parameter]
		public ReliefRequestDetailViewModel ReliefRequestVM { get; set; } = new ReliefRequestDetailViewModel();



	}
}