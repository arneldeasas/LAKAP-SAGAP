using StockItemModel = LAKAPSAGAP.Models.Models.StockItem;
using UoMModel = LAKAPSAGAP.Models.Models.UoM;
using CategoryModel = LAKAPSAGAP.Models.Models.Category;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.StockItem;

public partial class StockItemsControl
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] IUoMRepository _uomRepo { get; set; }
	[Inject] ICategoryRepository _categoryRepo { get; set; }
	[Inject] IStockItemRepository _stockItemRepo { get; set; }
	[Inject] IJSRuntime _jSRuntime { get; set; }

	List<StockItemViewModel> _stockItems { get; set; }
	List<StockItemViewModel> _filteredStockItems { get; set; }
	List<UoMViewModel> _uomList { get; set; }
	List<CategoryViewModel> _categoryList { get; set; }


	List<BreadcrumbViewModel> Breadcrumbs =
	[
		new() { Path = "/MasterData/StockItems", Text = "StockItems" },
	];

	RadzenDataGrid<StockItemViewModel> _stockItemsGrid { get; set; }

	bool _isBusy { get; set; }

	protected override void OnInitialized()
	{
		_stockItems = [];
		_filteredStockItems = [];
		_uomList = [];
		_categoryList = [];
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await LoadUomList();
			await LoadCategoryList();
			await LoadStockItemsList();
			_filteredStockItems = _stockItems;
			await RerenderTable();
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task LoadStockItemsList()
	{
		SetBusy(true);
		await LoadCategoryList();
		await LoadUomList();
		List<StockItemModel> stockItems = await _stockItemRepo.GetAllStockItem();
		_stockItems = [];
		foreach (StockItemModel stockItem in stockItems)
		{
			_stockItems.Add(new()
			{
				Id = stockItem.Id,
				Name = stockItem.Name,
				CategoryId = stockItem.CategoryId,
				UoMId = stockItem.UoMId,
				isArchived = stockItem.isArchived
			});
		}
		SetBusy(false);
	}

	async Task LoadUomList()
	{
		List<UoMModel> uomList = await _uomRepo.GetAllUoM();
		foreach (UoMModel uom in uomList)
		{
			_uomList.Add(new()
			{
				Id = uom.Id,
				Name = uom.Name,
				isArchived = uom.isArchived
			});
		}
	}
	
	async Task LoadCategoryList()
	{
		List<CategoryModel> categoryList = await _categoryRepo.GetAllCategory();
		foreach (CategoryModel category in categoryList)
		{
			_categoryList.Add(new()
			{
				Id = category.Id,
				Code = category.Code,
				Name = category.Name,
				isArchived = category.isArchived
			});
		}
	}

	async Task RerenderTable()
	{
		_filteredStockItems = _stockItems;
		await _stockItemsGrid.RefreshDataAsync();
		await _stockItemsGrid.Reload();
		StateHasChanged();
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}