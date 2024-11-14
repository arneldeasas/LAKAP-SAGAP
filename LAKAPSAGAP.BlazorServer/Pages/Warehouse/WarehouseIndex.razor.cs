﻿
using LAKAPSAGAP.Models.Models;

namespace LAKAPSAGAP.BlazorServer.Pages.Warehouse
{
	public partial class WarehouseIndex
	{

		[Inject] IWarehouseRepository? WarehouseRepo { get; set; }
		[Inject] NavigationManager? NavManager { get; set; }
		[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;


		private bool Loading = true;
		public List<Models.Models.Warehouse> WarehouseList { get; set; } = new();
		public WarehouseViewModel model { get; set; } = new();
		private RadzenDataList<FloorViewModel> floorDL { get; set; } = default!;
		private RadzenDataList<RackViewModel> rackDL { get; set; } = default!;
		public FloorViewModel floorModel { get; set; } = new();
		public RackViewModel rackModel { get; set; } = new();

		protected override async Task OnInitializedAsync()
		{
			//Loading = true;
			//await Task.Delay(1000);
			//WarehouseList = await WarehouseRepo.GetAllWarehouses();
			//if (WarehouseList.Count() > 0) NavManager.NavigateTo($@"/Warehouses/{WarehouseList.First().Id}",true);
			Loading = false;
        }

		public async Task AddFloor()
		{
			model.FloorList.Add(floorModel);
			floorModel = new();
			await floorDL.Reload();
			StateHasChanged();
		}

		public async Task RemoveFloor(FloorViewModel floor)
		{
			model.FloorList.Remove(floor);
			floorModel = new();
			await floorDL.Reload();
			StateHasChanged();
		}

		public async Task AddRack(string floorName, RackViewModel rack)
		{
			FloorViewModel floor = model.FloorList.Where(x => x.Name == floorName).Single();
			if (floor is null) return;
            floor.RackList.Add(rack);
			rackModel = new();
			await floorDL.Reload();
			StateHasChanged();
		}

		public async Task RemoveRack(string floorName, RackViewModel rack)
		{
			FloorViewModel floor = model.FloorList.Where(x => x.Name == floorName).Single();
			if (floor is null) return;
			floor.RackList.Remove(rack);
			rackModel = new();
			await floorDL.Reload();
			StateHasChanged();
		}

		public async Task CreateWarehouse()
		{
			if (!await _jSRuntime.InvokeAsync<bool>("Confirmation")) return;

			try
			{
				await WarehouseRepo.CreateWarehouse(model);
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}