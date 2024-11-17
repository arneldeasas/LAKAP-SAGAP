namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.UoM;

public partial class CreateUoMDialog
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] IUoMRepository _uomRepo { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; }

	UoMViewModel _newUom { get; set; }

	bool _isBusy { get; set; }

	protected override void OnInitialized()
	{
		_newUom = new ();
		base.OnInitialized();
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		string? newId = await _uomRepo.CreateUoM(_newUom);
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