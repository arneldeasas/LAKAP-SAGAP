namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.Rack;

public partial class CreateRackDialog
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] IRackRepository _rackRepo { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; }

	RackViewModel _newRack { get; set; }

	bool _isBusy { get; set; }
	bool _isActive { get; set; }

	protected override void OnInitialized()
	{
		_newRack = new ();
		_isActive = true;
		base.OnInitialized();
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		_newRack.isArchived = !_isActive;
		string? newId = await _rackRepo.CreateRack(_newRack);
		SetBusy(true);
		_dialogService.Close(newId);        

		// TODO: have the user be notified about the error

        //if (!string.IsNullOrEmpty(newId))
        //{
        //}
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}