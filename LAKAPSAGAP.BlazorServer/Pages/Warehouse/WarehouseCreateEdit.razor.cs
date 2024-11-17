
using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.BlazorServer.Pages.Warehouse
{
	public partial class WarehouseCreateEdit
	{

		[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
		[Inject] IWarehouseRepository? WarehouseRepo { get; set; }


		public List<Warehouse> warehouses { get; set; } = new();
		private RadzenDataList<FloorViewModel> floorDL { get; set; } = default!;
		private RadzenDataList<RackViewModel> rackDL { get; set; } = default!;
		public FloorViewModel floorModel { get; set; } = new();
		public RackViewModel rackModel { get; set; } = new();

		private string _selectedWhse = String.Empty;

		private bool _isCreating = true;
		private bool Loading = true;

		public async Task AddFloor()
		{
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

				if (whseModel.Id is not null)
				{
					await WarehouseRepo.UpdateWarehouse(whseModel);
					await _jSRuntime.InvokeVoidAsync("Toast", "success", "Warehouse Updated Successfully!");
					await Task.Delay(3000);
				}
				else if (whseModel.Id is null)
                {
					await WarehouseRepo.CreateWarehouse(whseModel);
					await _jSRuntime.InvokeVoidAsync("Toast", "success", "Warehouse Created Successfully!");
					await Task.Delay(3000);
				}

			}
			catch (Exception e)
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "error", $@"{e.Message}");
				//throw;
			}
			finally
			{
				dialogService.Close(true);
			}
		}

	}
}
