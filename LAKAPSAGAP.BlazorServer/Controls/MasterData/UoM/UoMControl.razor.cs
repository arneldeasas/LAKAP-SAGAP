using UoMModel = LAKAPSAGAP.Models.Models.UoM;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.UoM;

public partial class UoMControl
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] IUoMRepository _uomRepo { get; set; }

	RadzenDataGrid<UoMViewModel> _uomGrid { get; set; }
	
	List<BreadcrumbViewModel> Breadcrumbs =
	[
		new() { Path = "/MasterData/UoM", Text = "Unit Of Measurement" },
	];
	List<UoMViewModel> _uomList { get; set; }
	List<UoMViewModel> _uomListFiltered { get; set; }

	bool _isBusy { get; set; }

	protected override void OnInitialized()
	{
		_uomList = [new() { }];
		_uomListFiltered = [];
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if(firstRender)
		{
			await LoadUoMList();
			_uomListFiltered = _uomList;
			await RerenderTable();
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task LoadUoMList()
	{
		SetBusy(true);
		List<UoMModel> uoms = await _uomRepo.GetAllUoM();
		_uomList = [];
		foreach(UoMModel uom in uoms)
		{
			_uomList.Add(new()
			{
				Id = uom.Id,
				Name = uom.Name,
				Symbol = uom.Symbol,
			});
		}
		SetBusy(false);
	}

	async Task RerenderTable()
	{
		_uomListFiltered = _uomList;
		await _uomGrid.RefreshDataAsync();
		await _uomGrid.Reload();
		StateHasChanged();
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}