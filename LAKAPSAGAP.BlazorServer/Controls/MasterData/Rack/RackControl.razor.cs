using RackModel = LAKAPSAGAP.Models.Models.Rack;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.Rack;

public partial class RackControl
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] IRackRepository _rackRepo { get; set; }

	List<RackViewModel> _racks { get; set; }
	List<RackViewModel> _filteredRacks { get; set; }
	List<BreadcrumbViewModel> Breadcrumbs =
	[
		new() { Path = "/MasterData/Racks", Text = "Racks" },
	];

	RadzenDataGrid<RackViewModel> _racksGrid { get; set; }

	bool _isBusy { get; set; }

	protected override void OnInitialized()
	{
		_racks = [];
		_filteredRacks = [];
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await LoadRacksList();
			_filteredRacks = _racks;
			await RerenderTable();
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task LoadRacksList()
	{
		SetBusy(true);
		List<RackModel> racks = await _rackRepo.GetAllRack();
		_racks = [];
		foreach (RackModel rack in racks)
		{
			_racks.Add(new()
			{
				Id = rack.Id,
				Name = rack.Name,
				isArchived = rack.isArchived
			});
		}
		SetBusy(false);
	}

	async Task RerenderTable()
	{
		_filteredRacks = _racks;
		await _racksGrid.RefreshDataAsync();
		await _racksGrid.Reload();
		StateHasChanged();
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}