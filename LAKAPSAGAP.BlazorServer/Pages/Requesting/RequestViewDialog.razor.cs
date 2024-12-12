namespace LAKAPSAGAP.BlazorServer.Pages.Requesting
{
	public partial class RequestViewDialog
	{
		[Inject] IJSRuntime _jsRuntime { get; set; }
		[Inject] DialogService _dialogService { get; set; }
		[Inject] IReliefRequestRepository _requestRepo { get; set; }
		[Parameter]
		public ReliefRequestDetailViewModel ReliefRequestVM { get; set; } = new ReliefRequestDetailViewModel();


		async Task RejectRequest()
		{
			try
			{
				if(!(await _jsRuntime.InvokeAsync<bool>("Confirmation"))) return;
				 if(await _requestRepo.RejectRequest(ReliefRequestVM.Id))
				{
					_dialogService.Close();
					await _jsRuntime.InvokeVoidAsync("ShowToast", "Request updated successfully.", "success");

				}

			}
			catch (Exception e)
			{

				await _jsRuntime.InvokeVoidAsync("ShowToast", e.Message, "error");
			}
		}

		async Task ApproveRequest(string id)
		{
			try
			{
				if (!(await _jsRuntime.InvokeAsync<bool>("Confirmation"))) return;
			
				if (await _requestRepo.ApproveRequest(id))
				{
					_dialogService.Close("reload");
					await _jsRuntime.InvokeVoidAsync("ShowToast", "Request updated successfully.", "success");

				}

			}
			catch (Exception e)
			{

				await _jsRuntime.InvokeVoidAsync("ShowToast", e.Message, "error");
			}
		}
	}
}