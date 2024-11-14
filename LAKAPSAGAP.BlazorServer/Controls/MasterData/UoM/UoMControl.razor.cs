
namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.UoM;

public partial class UoMControl
{
	[Inject] DialogService _dialogService { get; set; }

	RadzenDataGrid<UoMViewModel> _uomGrid { get; set; }
	
	List<BreadcrumbViewModel> Breadcrumbs =
	[
		new() { Path = "/MasterData/UoM", Text = "Unit Of Measurement" },
	];
	List<UoMViewModel> _uomList { get; set; }
	List<UoMViewModel> _uomListFiltered { get; set; }

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
			//await LoadUoMList(); // will change later
			_uomList = [new() { Name = "Kilogram", Symbol = "Kg" }];

			_uomListFiltered = _uomList;
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task LoadUoMList()
	{
		//TODO: get all uom data/ uom list
		await _uomGrid.RefreshDataAsync();
		await _uomGrid.Reload();
	}
}