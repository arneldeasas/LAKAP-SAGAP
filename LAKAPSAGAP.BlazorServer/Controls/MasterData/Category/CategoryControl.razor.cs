using CategoryModel = LAKAPSAGAP.Models.Models.Category;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.Category;

public partial class CategoryControl
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] ICategoryRepository _categoryRepo { get; set; }

	List<CategoryViewModel> _categories { get; set; }
	List<CategoryViewModel> _filteredCategories { get; set; }
	List<BreadcrumbViewModel> Breadcrumbs =
	[
		new() { Path = "/MasterData/Categories", Text = "Categories" },
	];

	RadzenDataGrid<CategoryViewModel> _categoriesGrid { get; set; }

	bool _isBusy { get; set; }

	protected override void OnInitialized()
	{
		_categories = [];
		_filteredCategories = [];
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await LoadUoMList();
			_filteredCategories = _categories;
			await RerenderTable();
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task LoadUoMList()
	{
		SetBusy(true);
		List<CategoryModel> uoms = await _categoryRepo.GetAllCategory();
		_categories = [];
		foreach (CategoryModel category in uoms)
		{
			_categories.Add(new()
			{
				Id = category.Id,
				Code = category.Code,
				Name = category.Name,
				isArchived = category.isArchived
			});
		}
		SetBusy(false);
	}

	async Task RerenderTable()
	{
		_filteredCategories = _categories;
		await _categoriesGrid.RefreshDataAsync();
		await _categoriesGrid.Reload();
		StateHasChanged();
	}

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}