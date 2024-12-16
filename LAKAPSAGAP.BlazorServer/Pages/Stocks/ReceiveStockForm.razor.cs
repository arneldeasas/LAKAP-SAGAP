using LAKAPSAGAP.Models.Enums;
using Mapster;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks;

public partial class ReceiveStockForm
{
	[Parameter] public string WarehouseId { get; set; }

	[Inject] IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] DialogService _dialogService { get; set; }
	[Inject] IStockItemRepository _stockItemRepository { get; set; }
	[Inject] ICategoryRepository _categoryRepository { get; set; }
	[Inject] IUoMRepository _uomRepository { get; set; }
	[Inject] IReliefReceivedRepository _reliefReceivedRepository { get; set; }
	[Inject] IRackRepository _rackRepository { get; set; }

	List<AcquisitionTypes> _acquisitionTypes { get; set; }
	List<StockItemViewModel> _stockItems { get; set; }
	List<CategoryViewModel> _itemCategories { get; set; }
	List<UoMViewModel> _uomList { get; set; }
	List<FloorViewModel> _floors { get; set; }
	List<RackViewModel> _racks { get; set; }

	IJSObjectReference _js { get; set; } = default!;
	ReceiveReliefVM _newReceivedRelief { get; set; }
	StockItemViewModel _selectedStockViewModel { get; set; }
	StockDetailVM? _selectedStockDetail { get; set; }

	RadzenDataGrid<StockDetailVM> _stockDetailsGrid { get; set; }
	RadzenDropDownDataGrid<StockItemViewModel> _stockItemDropDownDataGrid { get; set; }

	string _activePane { get; set; } = "form";

	bool _hoveringItemDropDownFld { get; set; }

	protected override void OnInitialized()
	{
		_newReceivedRelief ??= new() { WarehouseId = WarehouseId };
		_stockItems ??= [];
		_itemCategories ??= [];
		_uomList ??= [];
		_floors ??= [];
		_racks ??= [];
		_selectedStockViewModel ??= new();
		_acquisitionTypes = Enum.GetValues(typeof(AcquisitionTypes)).Cast<AcquisitionTypes>().ToList();
		base.OnInitialized();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await LoadStockItemsList();
			await LoadCategoryList();
			await LoadUomList();
			await LoadFloorsList();

			await InitializeEditRow(false);

			StateHasChanged();

		}
		await base.OnAfterRenderAsync(firstRender);
	}

	async Task LoadStockItemsList()
	{
		_stockItems = [];
		List<StockItem> stockItems = await _stockItemRepository.GetAllActiveStockItem();
		foreach (StockItem stockItem in stockItems)
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
	}

	async Task LoadCategoryList()
	{
		_itemCategories = [];
		List<Category> categories = await _categoryRepository.GetAllActiveCategories();
		foreach (Category category in categories)
		{
			_itemCategories.Add(new()
			{
				Id = category.Id,
				Name = category.Name,
				Code = category.Code,
				isArchived = category.isArchived
			});
		}
	}

	void UpdateItemCategoryOnEditingLine(string categoryId)
	{
		CategoryViewModel category = _itemCategories.First(x => x.Id == categoryId);
		_newReceivedRelief.StockDetailList[0].CategoryId = category.Id;
		_newReceivedRelief.StockDetailList[0].CategoryName = category.Name;
		_newReceivedRelief.StockDetailList[0].CategoryCode = category.Code;
	}

	async Task LoadUomList()
	{
		_uomList = [];
		List<UoM> uomList = await _uomRepository.GetAllActiveUoM();
		foreach (UoM uom in uomList)
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

	void UpdateItemUoMOnEditingLine(string uomId)
	{
		UoMViewModel category = _uomList.First(x => x.Id == uomId);
		_newReceivedRelief.StockDetailList[0].UomId = category.Id;
		_newReceivedRelief.StockDetailList[0].UomName = category.Name;
	}

	async Task LoadFloorsList()
	{
		_floors = [];
		List<Floor> floors = await _reliefReceivedRepository.GetAllFloorsActiveBasedOnWarehouse(WarehouseId);
		foreach (Floor floor in floors)
		{
			_floors.Add(new()
			{
				Id = floor.Id,
				Name = floor.Name,
			});
		}
	}

	async Task SelectedFloorChanged(StockDetailVM stockDetail)
	{
		stockDetail.FloorName = _floors.First(x => x.Id == stockDetail.FloorId).Name;
		stockDetail.RackId = string.Empty;
		stockDetail.RackName = string.Empty;

		_racks = [];
		List<Rack> racks = await _reliefReceivedRepository.GetAllRacksBasedOnFloor(stockDetail.FloorId);
		foreach(Rack rack in racks)
		{
			_racks.Add(new()
			{
				Id = rack.Id,
				FloorId = rack.FloorId,
				Name= rack.Name,
				isArchived = rack.isArchived
			});
		}
		StateHasChanged();
	}

	async Task SelectedRackChanged(StockDetailVM stockDetailVM)
	{
		stockDetailVM.RackName = _racks.First(x => x.Id == stockDetailVM.RackId).Name;
		StateHasChanged();
	}

	void ItemChanged(StockItemViewModel stockItem)
	{
		if (stockItem is null) return;
		_newReceivedRelief.StockDetailList[0].StockItemId = stockItem.Id;
		_newReceivedRelief.StockDetailList[0].StockItemName = stockItem.Name;
		UpdateItemCategoryOnEditingLine(stockItem.CategoryId);
		UpdateItemUoMOnEditingLine(stockItem.UoMId);
		StateHasChanged();
	}

	void ClearItem(bool otherFieldsToo = false)
	{
		_newReceivedRelief.StockDetailList[0].StockItemId = string.Empty;
		_newReceivedRelief.StockDetailList[0].StockItemName = string.Empty;
		_newReceivedRelief.StockDetailList[0].CategoryId = string.Empty;
		_newReceivedRelief.StockDetailList[0].CategoryName = string.Empty;
		_newReceivedRelief.StockDetailList[0].CategoryCode = string.Empty;
		_newReceivedRelief.StockDetailList[0].UomId = string.Empty;
		_newReceivedRelief.StockDetailList[0].UomName = string.Empty;
		_selectedStockViewModel = new();

		if (otherFieldsToo)
		{
			_newReceivedRelief.StockDetailList[0].Quantity = 0;
			_newReceivedRelief.StockDetailList[0].FloorId = string.Empty;
			_newReceivedRelief.StockDetailList[0].FloorName = string.Empty;
			_newReceivedRelief.StockDetailList[0].RackId = string.Empty;
			_newReceivedRelief.StockDetailList[0].RackName = string.Empty;
			_newReceivedRelief.StockDetailList[0].ExpiryDate = DateTime.Now;
		}

		StateHasChanged();
	}

	async Task InitializeEditRow(bool initiatedAlready = false)
	{
		if (!initiatedAlready)
		{
			StockDetailVM newStockDetail = new();
			_newReceivedRelief.StockDetailList.Add(newStockDetail);
			await _stockDetailsGrid.Reload();
			await _stockDetailsGrid.EditRow(newStockDetail);
		}
		else
		{
			ClearItem(initiatedAlready);
			await _stockDetailsGrid.Reload();
			await _stockDetailsGrid.EditRow(_newReceivedRelief.StockDetailList[0]);
		}
		StateHasChanged();
	}

	async Task EditInsertedRow(StockDetailVM stockDetailVM)
	{
		_selectedStockDetail = stockDetailVM;
		await _stockDetailsGrid.EditRow(stockDetailVM);
	}

	async Task SaveEditRow(StockDetailVM stockDetailVM)
	{
		if ((await _jSRuntime.InvokeAsync<bool>("Confirmation")))
		{
			_newReceivedRelief.StockDetailList.Remove(_selectedStockDetail);
			_newReceivedRelief.StockDetailList.Add(stockDetailVM);
			await _stockDetailsGrid.UpdateRow(stockDetailVM);
			await _stockDetailsGrid.Reload();
			_selectedStockDetail = null;
			StateHasChanged();
		}
	}

	async Task CancelEditRow(StockDetailVM stockDetailVM)
	{
		if ((await _jSRuntime.InvokeAsync<bool>("Confirmation")))
		{
			_selectedStockDetail = null;
			_stockDetailsGrid.CancelEditRow(stockDetailVM);
			StateHasChanged();
		}
	}

	async Task RemoveStockDetail(StockDetailVM stockDetailVM)
	{
		if ((await _jSRuntime.InvokeAsync<bool>("Confirmation")))
		{
			_newReceivedRelief.StockDetailList.Remove(stockDetailVM);
			await _stockDetailsGrid.Reload();
			StateHasChanged();
		}
	}

	async Task AddStockDetails()
	{
		if (_selectedStockDetail is not null)
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Cancel or save any editing rows first.");
			return;
		}

		if (!(await ValidateLine())) return;

		StockDetailVM stockDetail = new()
		{
			StockItemId = _newReceivedRelief.StockDetailList[0].StockItemId,
			StockItemName = _newReceivedRelief.StockDetailList[0].StockItemName,
			CategoryId = _newReceivedRelief.StockDetailList[0].CategoryId,
			CategoryName = _newReceivedRelief.StockDetailList[0].CategoryName,
			CategoryCode = _newReceivedRelief.StockDetailList[0].CategoryCode,
			UomId = _newReceivedRelief.StockDetailList[0].UomId,
			UomName = _newReceivedRelief.StockDetailList[0].UomName,
			Quantity = _newReceivedRelief.StockDetailList[0].Quantity,
			FloorId = _newReceivedRelief.StockDetailList[0].FloorId,
			FloorName = _newReceivedRelief.StockDetailList[0].FloorName,
			RackId = _newReceivedRelief.StockDetailList[0].RackId,
			RackName = _newReceivedRelief.StockDetailList[0].RackName,
			ExpiryDate = _newReceivedRelief.StockDetailList[0].ExpiryDate
		};

		_newReceivedRelief.StockDetailList.Add(stockDetail);
		await InitializeEditRow(true);
	}

	async Task<bool> ValidateLine()
	{
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].StockItemId))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Stock Item Id cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].StockItemName))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Stock Item Name cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].CategoryId))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Category Id cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].CategoryName))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Category Name cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].CategoryCode))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Category Code cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].UomId))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Uom Id cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].UomName))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Uom Name cannot be empty or null");
			return false;
		}
		if (_newReceivedRelief.StockDetailList[0].Quantity <= 0)
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Quantity cannot be 0 or negative");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].FloorId))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Floor Id cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].FloorName))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Floor Name cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].RackId))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Rack Id cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].RackName))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Rack Name cannot be empty or null");
			return false;
		}
		if (string.IsNullOrEmpty(_newReceivedRelief.StockDetailList[0].ExpiryDate.ToString()))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Expiry Date cannot be empty or null");
			return false;
		}

		return true;
	}

	async Task Submit()
	{
		if(!(await _jSRuntime.InvokeAsync<bool>("Confirmation"))) return;

		_newReceivedRelief.StockDetailList.Remove(_newReceivedRelief.StockDetailList[0]);
		ReliefReceived newReliefReceived = _newReceivedRelief.Adapt<ReliefReceived>();
		newReliefReceived = await _reliefReceivedRepository.CreateReliefReceived(newReliefReceived);

		if (newReliefReceived is null)
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "error", "Failed to create stock details");
			return;
		}

		await _jSRuntime.InvokeVoidAsync("Toast", "success", "Relief received recorded successfully");
		_dialogService.Close(true);
	}

	public void changeTab(string tab)
	{
		_activePane = tab;
	}

	// Classes
	class ReceiveReliefVM
	{
		public AcquisitionTypes AcquisitionType { get; set; }
		public string ReceivedBy { get; set; }

		// Delivery Details
		public string PlateNo { get; set; }
		public string DriverName { get; set; }
		public string? ReceivedFrom { get; set; }
		public DateTime ReceivedDate { get; set; } = DateTime.Now;
		public List<StockDetailVM> StockDetailList { get; set; } = [];

		public string WarehouseId { get; set; }
	}

	class StockDetailVM
	{
		public string StockItemId { get; set; }
		public string StockItemName { get; set; }
		public string CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string CategoryCode { get; set; }
		public string UomId { get; set; }
		public string UomName { get; set; }
		public int Quantity { get; set; }
		public DateTime ExpiryDate { get; set; }
		public string FloorId { get; set; }
		public string FloorName { get; set; }
		public string RackId { get; set; }
		public string RackName { get; set; }
	}
}
