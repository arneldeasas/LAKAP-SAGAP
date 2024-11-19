using RackModel = LAKAPSAGAP.Models.Models.Rack;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.Rack;

public partial class ViewUpdateRackDialog
{
	[Parameter] public string Id { get; set; }

	[Inject] DialogService _dialogService { get; set; }
	[Inject] IRackRepository _rackRepo { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; }

	RackViewModel _rack { get; set; }

	bool _isBusy { get; set; }
	bool _isEditing { get; set; }
	bool _isActive { get; set; }

	protected override void OnInitialized()
	{
		_rack = new();
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if(firstRender)
		{
			RackModel? Rack = await _rackRepo.GetRack(Id);
			if(Rack is not null)
			{
				_rack.Id = Rack.Id;
				_rack.Name = Rack.Name;
				_rack.isArchived = Rack.isArchived;
			}
			else
			{
				//TODO: Toast an error
				// Initializes the cateogry as new to avoid errors
				_rack = new();
			}
			
			_isActive = !_rack.isArchived;
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		_rack.isArchived = !_isActive;
		bool success = await _rackRepo.UpdateRack(_rack);
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