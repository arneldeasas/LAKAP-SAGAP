using UoMModel = LAKAPSAGAP.Models.Models.UoM;
using CategoryModel = LAKAPSAGAP.Models.Models.Category;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.StockItem;

public partial class CreateStockItemDialog
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] IJSRuntime _jSRuntime { get; set; }
	[Inject] IUoMRepository _uomRepo { get; set; }
	[Inject] ICategoryRepository _categoryRepo { get; set; }
	[Inject] IStockItemRepository _stockItemRepo { get; set; }

	List<UoMViewModel> _uomList { get; set; }
	List<CategoryViewModel> _categoryList { get; set; }

	StockItemViewModel _newStockItem { get; set; }

	bool _isBusy { get; set; }
	bool _isActive { get; set; }
	bool _categoryFldHovering { get; set; }
	bool _uomFldHovering { get; set; }

	protected override void OnInitialized()
	{
		_newStockItem = new ();
		_isActive = true;
		_uomList = [];
		_categoryList = [];
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if(firstRender)
		{
			await LoadCategoryList();
			await LoadUomList();

			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		_newStockItem.isArchived = !_isActive;
		string? newId = await _stockItemRepo.CreateStockItem(_newStockItem);
		SetBusy(true);
		
		if (!string.IsNullOrEmpty(newId)) await _jSRuntime.InvokeVoidAsync("Toast", "success", "Stock Item added successfully!");
		else await _jSRuntime.InvokeVoidAsync("Toast", "error", "An error occured. Something went wrong!");

		_dialogService.Close(newId);
	}

	async Task LoadUomList()
	{
		_uomList = [];
		List<UoMModel> uomList = await _uomRepo.GetAllActiveUoM();
		foreach (UoMModel uom in uomList)
		{
			_uomList.Add(new()
			{
				Id = uom.Id,
				Name = uom.Name,
				Symbol = uom.Symbol,
				isArchived = uom.isArchived
			});
		}
	}

	async Task LoadCategoryList()
	{
		_categoryList = [];
		List<CategoryModel> categoryList = await _categoryRepo.GetAllActiveCategories();
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

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}