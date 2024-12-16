using Mapster;

namespace LAKAPSAGAP.BlazorServer.Pages.Requesting
{
	
	public partial class RequestsAdmin
	{
		[Inject] DialogService _dialogService { get; set; }
		[Inject] IJSRuntime _jsRuntime { get; set; }
		[Inject] IReliefRequestRepository _requestRepo { get; set; }

		private List<ReliefRequestDetailViewModel> _ongoingRequestListVM = new List<ReliefRequestDetailViewModel>();
		private List<ReliefRequestDetailViewModel> _doneRequestListVM = new List<ReliefRequestDetailViewModel>();
		private bool _isBusy = true;

		protected override async Task OnInitializedAsync()
		{
			await LoadRequests();
		}

		private async Task LoadRequests()
		{
			_isBusy = true;
			StateHasChanged();
			List<ReliefRequestDetail> ongoingReliefRequests = await _requestRepo.GetOnGoingRequests();
			if (ongoingReliefRequests.Count > 0)
			{
				_ongoingRequestListVM = ongoingReliefRequests.Select(x => new ReliefRequestDetailViewModel
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
                    RequestedBy = new UserInfoViewModel
                    {
                        FirstName = x.RequestedBy.FirstName,
                        LastName = x.RequestedBy.LastName,
                    },
                    ReceiverName = x.ReceiverName,
					ContactNumber = x.ContactNumber,
					AttachmentList = x.AttachmentList.Select(x => new RequestAttachmentViewModel
					{
						Url = x.Url,
					}).ToList(),
					DateRequested = x.DateCreated
				}).ToList();
			}
			List<ReliefRequestDetail> doneReliefRequests = await _requestRepo.GetDoneRequests();
			if(doneReliefRequests.Count > 0)
			{
				_doneRequestListVM = doneReliefRequests.Select(x => new ReliefRequestDetailViewModel
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
                    RequestedBy = new UserInfoViewModel
                    {
                        FirstName = x.RequestedBy.FirstName,
                        LastName = x.RequestedBy.LastName,
                    },
                    ReceiverName = x.ReceiverName,
					ContactNumber = x.ContactNumber,
					AttachmentList = x.AttachmentList.Select(x => new RequestAttachmentViewModel
					{
						Url = x.Url,
					}).ToList(),
					DateRequested = x.DateCreated
				}).ToList();
			}


			_isBusy = false;
			StateHasChanged();
		}
		private async Task CancelRequest(int id)
		{
			//await _requestService.CancelRequest(id);
			await LoadRequests();
		}
	}
}