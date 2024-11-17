using UoMModel = LAKAPSAGAP.Models.Models.UoM;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.UoM;

public partial class ViewUpdateUoMDialog
{
	[Parameter] public string Id { get; set; }

	[Inject] DialogService _dialogService { get; set; }
	[Inject] IUoMRepository _uomRepo { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; }

	UoMViewModel _uom { get; set; }

	bool _isBusy { get; set; }
	bool _isEditing { get; set; }
	bool _isActive { get; set; }

	protected override void OnInitialized()
	{
		_uom = new();
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if(firstRender)
		{
			UoMModel? uom = await _uomRepo.GetUoM(Id);
			if(uom is not null)
			{
				_uom.Id = uom.Id;
				_uom.Name = uom.Name;
				_uom.Symbol = uom.Symbol;
				_uom.isArchived = uom.isArchived;
			}
			else
			{
				//TODO: Toast an error
				// Initializes the uom as new to avoid errors
				_uom = new();
			}
			
			_isActive = !_uom.isArchived;
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		_uom.isArchived = !_isActive;
		bool success = await _uomRepo.UpdateUoM(_uom);
		SetBusy(true);

		// TODO: have the user be notified about the process
		//if (success)
		//else

		_dialogService.Close();
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}