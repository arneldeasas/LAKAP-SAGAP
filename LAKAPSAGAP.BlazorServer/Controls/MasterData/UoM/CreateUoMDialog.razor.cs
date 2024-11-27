namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.UoM;

public partial class CreateUoMDialog
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] IUoMRepository _uomRepo { get; set; }
	[Inject] IJSRuntime _jSRuntime { get; set; }

	UoMViewModel _newUom { get; set; }

	bool _isBusy { get; set; }
	bool _isActive { get; set; }

	protected override void OnInitialized()
	{
		_newUom = new ();
		_isActive = true;
		base.OnInitialized();
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		_newUom.isArchived = !_isActive;
		string? newId = await _uomRepo.CreateUoM(_newUom);
		SetBusy(true);

		if (!string.IsNullOrEmpty(newId)) _jSRuntime.InvokeVoidAsync("Toast", "success", "UoM added successfully!");
		else _jSRuntime.InvokeVoidAsync("Toast", "error", "An error occured. Something went wrong!");
		
		_dialogService.Close(newId);
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}