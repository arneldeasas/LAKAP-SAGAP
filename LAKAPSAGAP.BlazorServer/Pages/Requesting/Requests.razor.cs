namespace LAKAPSAGAP.BlazorServer.Pages.Requesting
{
	
	public partial class Requests
	{
		[Inject] NavigationManager _navManager { get; set; }
		[Inject] IReliefRequestRepository _reliefRequestRepo { get; set; }
		List<ReliefRequestDetailViewModel> _requestListVM = new ();
		bool _isBusy = false;

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				_isBusy = true;
				StateHasChanged();
				await LoadTableData();
				_isBusy = false;
				StateHasChanged();
			}
		}

		async Task LoadTableData()
		{
			_isBusy = true;
			StateHasChanged();
			List<ReliefRequestDetail> reliefRequests = await _reliefRequestRepo.GetAllRequestsAsync();
			_requestListVM = reliefRequests.Select(x => new ReliefRequestDetailViewModel
			{
				Id = x.Id,

				Reason = x.Reason,
				Status = x.Status,
				SpecificReason = x.SpecificReason,
				NumberOfRecipients = x.NumberOfRecipients,
				Organization = x.Organization,
				RequestList = x.RequestList.Select(x => new RequestViewModel
				{
					Id = x.Id,
					UnitName = x.UnitName,
					Quantity = x.Quantity,

				}).ToList(),
				TargetDateToReceive = x.TargetDateToReceive,
				ReceiverAddress = x.ReceiverAddress,
				AdditionalNotes = x.AdditionalNotes,
				ReceiverName = x.ReceiverName,
				ContactNumber = x.ContactNumber,
				AttachmentList = x.AttachmentList.Select(x => new RequestAttachmentViewModel
				{
					Url = x.Url,
				}).ToList(),
				DateRequested = x.DateCreated
			}).ToList();
		}
			void GoToForm()
		{
			_navManager.NavigateTo("/requests/requestform");
		}
	}
}