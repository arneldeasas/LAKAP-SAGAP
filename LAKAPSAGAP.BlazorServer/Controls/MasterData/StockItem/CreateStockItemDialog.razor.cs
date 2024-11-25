using UoMModel = LAKAPSAGAP.Models.Models.UoM;
using CategoryModel = LAKAPSAGAP.Models.Models.Category;

namespace LAKAPSAGAP.BlazorServer.Controls.MasterData.StockItem;

public partial class CreateStockItemDialog
{
	[Inject] DialogService _dialogService { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; }
	[Inject] IUoMRepository _uomRepo { get; set; }
	[Inject] ICategoryRepository _categoryRepo { get; set; }
	//[Inject] IRackRepository _rackRepo { get; set; }

	List<UoMViewModel> _uomList { get; set; }
	List<CategoryViewModel> _categoryList { get; set; }

	StockItemViewModel _newStockItem { get; set; }

	bool _isBusy { get; set; }
	bool _isActive { get; set; }

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
		//string? newId = await _rackRepo.CreateRack(_newStockItem);
		SetBusy(true);
		//_dialogService.Close(newId);        

		// TODO: have the user be notified about the error

        //if (!string.IsNullOrEmpty(newId))
        //{
        //}
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