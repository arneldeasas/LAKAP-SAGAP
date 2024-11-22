
using LAKAPSAGAP.BlazorServer.Pages.MasterData.Rack;
using LAKAPSAGAP.Models.Models;
using Microsoft.IdentityModel.Tokens;

namespace LAKAPSAGAP.BlazorServer.Pages.Warehouse;

public partial class WarehouseCreateEdit
{

	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] IWarehouseRepository? WarehouseRepo { get; set; }


	public List<Models.Models.Warehouse> warehouses { get; set; } = new();
	private RadzenDataList<FloorViewModel> floorDL { get; set; } = default!;
	private RadzenDataList<RackViewModel> rackDL { get; set; } = default!;
	public FloorViewModel floorModel { get; set; } = new();
	public RackViewModel rackModel { get; set; } = new();

	private string _selectedWhse = String.Empty;

	private bool _isCreating = true;
	private bool Loading = true;

	protected override async Task OnInitializedAsync()
	{
		warehouses = await WarehouseRepo.GetAllWarehouses();
	}

	public async Task AddFloor()
	{
		if (string.IsNullOrEmpty(floorModel.Name))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "error", "Floor Name is required!");
			return;
		}
		if (whseModel.FloorList.Any(x => x.Name == floorModel.Name))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "error", "Floor Name already is use!");
			return;
		}
		whseModel.FloorList.Add(floorModel);
		floorModel = new();
		await floorDL.Reload();
		StateHasChanged();
	}

	public async Task RemoveFloor(FloorViewModel floor)
	{
		whseModel.FloorList.Remove(floor);
		floorModel = new();
		await floorDL.Reload();
		StateHasChanged();
	}

	public async Task AddRack(string floorName, RackViewModel rack)
	{
		FloorViewModel floor = whseModel.FloorList.Where(x => x.Name == floorName).Single();
		if (floor is null) return;
		if (string.IsNullOrEmpty(rack.Name))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "error", "Rack Name is required!");
			return;
		}
		if (floor.RackList.Any(x => x.Name == floorModel.Name))
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "error", "Rack Name already is use!");
			return;
		}
		floor.RackList.Add(rack);
		rackModel = new();
		await floorDL.Reload();
		StateHasChanged();
	}

	public async Task RemoveRack(string floorName, RackViewModel rack)
	{
		FloorViewModel floor = whseModel.FloorList.Where(x => x.Name == floorName).Single();
		if (floor is null) return;
		floor.RackList.Remove(rack);
		rackModel = new();
		await floorDL.Reload();
		StateHasChanged();
	}

	public async Task Save()
	{
		if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

		try
		{
			Loading = true;

			if (warehouses.Count > 0 && warehouses.Any(x => x.Name == whseModel.Name))
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "error", "A Warehouse with the same name already exists!");
				return;
			}

			if (whseModel.FloorList.Count <= 0)
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "error", "A Warehouse should have at least one Floor.");
				return;
			}

			if (whseModel.FloorList.Any(x => x.RackList.Count <= 0))
			{
				if (!await _jSRuntime.InvokeAsync<bool>("Confirmation", "One of your Floors have no racks!", "warning")) return;
			}

			if (string.IsNullOrWhiteSpace(whseModel.Name) || string.IsNullOrWhiteSpace(whseModel.Location))
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "error", "Warehouse Name and Location is Required.");
				return;
			}

			if (whseModel.Id is not null)
			{
				await WarehouseRepo.UpdateWarehouse(whseModel);
				await _jSRuntime.InvokeVoidAsync("Toast", "success", "Warehouse Updated Successfully!");
				await Task.Delay(3000);
				dialogService.Close(true);

			}
			else if (whseModel.Id is null)
			{
				await WarehouseRepo.CreateWarehouse(whseModel);
				await _jSRuntime.InvokeVoidAsync("Toast", "success", "Warehouse Created Successfully!");
				await Task.Delay(3000);
				dialogService.Close(true);

			}

		}
		catch (Exception e)
		{
			await _jSRuntime.InvokeVoidAsync("Toast", "error", $@"{e.Message}");
			//throw;
		}
	}

}
