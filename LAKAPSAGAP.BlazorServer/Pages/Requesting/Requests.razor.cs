namespace LAKAPSAGAP.BlazorServer.Pages.Requesting
{
	
	public partial class Requests
	{
		[Inject] NavigationManager _navManager { get; set; }
		List<ReliefRequestDetailViewModel> _requestListVM = new ();
		bool _isBusy = false;

		void GoToForm()
		{
			_navManager.NavigateTo("/requests/requestform");
		}
	}
}