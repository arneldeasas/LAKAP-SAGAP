namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.UoM;

public partial class CreateUoMDialog
{
	[Inject] DialogService _dialogService { get; set; }

	UoMViewModel _newUom { get; set; }

	protected override void OnInitialized()
	{
		_newUom = new ();
		base.OnInitialized();
	}

	async Task Submit()
	{
		_dialogService.Close("new id");
	}
}