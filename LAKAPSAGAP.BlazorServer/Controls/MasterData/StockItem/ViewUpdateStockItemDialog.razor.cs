using StockItemModel = LAKAPSAGAP.Models.Models.StockItem;
using UoMModel = LAKAPSAGAP.Models.Models.UoM;
using CategoryModel = LAKAPSAGAP.Models.Models.Category;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.StockItem;

public partial class ViewUpdateStockItemDialog
{
	[Parameter] public string Id { get; set; }

	[Inject] DialogService _dialogService { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; }
	[Inject] IUoMRepository _uomRepo { get; set; }
	[Inject] ICategoryRepository _categoryRepo { get; set; }
	//[Inject] IRackRepository _rackRepo { get; set; } // Change this with Stock item repo

	List<UoMViewModel> _uomList { get; set; }
	List<CategoryViewModel> _categoryList { get; set; }

	StockItemViewModel _stockItem { get; set; }

	bool _isBusy { get; set; }
	bool _isEditing { get; set; }
	bool _isActive { get; set; }

	protected override void OnInitialized()
	{
		_stockItem = new();
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

			//StockItemModel? stockItem = await _rackRepo.GetRack(Id);
			//if(stockItem is not null)
			//{
			//	_stockItem.Id = stockItem.Id;
			//	_stockItem.Name = stockItem.Name;
			//	_stockItem.CategoryId = stockItem.CategoryId;
			//	_stockItem.UoMId = stockItem.UoMId;
			//	_stockItem.isArchived = stockItem.isArchived;
			//}
			//else
			//{
			//	//TODO: Toast an error
			//	// Initializes the cateogry as new to avoid errors
			//	_stockItem = new();
			//}
			
			_isActive = !_stockItem.isArchived;
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task Submit()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		SetBusy(true);
		_stockItem.isArchived = !_isActive;
		//bool success = await _rackRepo.UpdateRack(_stockItem);
		SetBusy(true);

		// TODO: have the user be notified about the process
		//if (success)
		//else

		_dialogService.Close();
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

	void SetBusy(bool busy)
	{
		_isBusy = busy;
		StateHasChanged();
	}
}