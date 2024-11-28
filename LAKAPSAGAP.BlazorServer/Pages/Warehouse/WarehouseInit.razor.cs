
using LAKAPSAGAP.Models.Models;
using Microsoft.IdentityModel.Tokens;

namespace LAKAPSAGAP.BlazorServer.Pages.Warehouse
{
	public partial class WarehouseInit
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
			Loading = true;
			await Task.Delay(1000);
			WarehouseList = await WarehouseRepo.GetAllWarehouses();
			if (WarehouseList.Count() > 0) NavManager.NavigateTo($@"/Warehouse/{WarehouseList.First().Id}", true);
			Loading = false;
        }

		public async Task AddFloor()
		{
			if (floorModel.Name.IsNullOrEmpty())
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "error", "Floor Name is required!");
				return;
			}
				if (model.FloorList.Any(x => x.Name.ToLower() == floorModel.Name.ToLower()))
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "error", "Floor Name already is use!");
				return;
			}
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
            if (string.IsNullOrWhiteSpace(rack.Name))
			{
				await _jSRuntime.InvokeVoidAsync("Toast", "error", "Rack Name is required!");
				return;
			}
            FloorViewModel floor = model.FloorList.Where(x => x.Name.ToLower() == floorName.ToLower()).Single();
			if (floor is null) return;
			if (floor.RackList.Any(x => x.Name.ToLower() == rack.Name.ToLower()))
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
			FloorViewModel floor = model.FloorList.Where(x => x.Name.ToLower() == floorName.ToLower()).Single();
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
				var whse = await WarehouseRepo.CreateWarehouse(model);
				Loading = true;

                if (WarehouseList.Count > 0 && WarehouseList.Any(x => x.Name.ToLower() == whse.Name.ToLower()))
                {
					await _jSRuntime.InvokeVoidAsync("Toast", "error", "A Warehouse with the same name already exists!");
					return;
				}

				if (whse.FloorList.Count <= 0)
                {
					await _jSRuntime.InvokeVoidAsync("Toast", "error", "A Warehouse should have at least one Floor.");
					return;
				}

				if (whse.FloorList.Any(x => x.Racks.Count <= 0))
				{
					if (!await _jSRuntime.InvokeAsync<bool>("Confirmation", "One of your Floors have no racks!", "warning")) return;
				}

				if (string.IsNullOrWhiteSpace(whse.Name) || string.IsNullOrWhiteSpace(whse.Location))
				{
					await _jSRuntime.InvokeVoidAsync("Toast", "error", "Warehouse Name and Location is Required.");
					return;
				}

				if (whse is not null)
                {
					await _jSRuntime.InvokeVoidAsync("Toast", "success", "Warehouse Created Successfully!");
					await Task.Delay(3000);
					NavManager.NavigateTo($"/Warehouse/{whse.Id}/Stocks", true);
                }
				else
				{
					await _jSRuntime.InvokeVoidAsync("Toast", "warning", "Oh No! Something bad happenned, Please try again...");
					await Task.Delay(3000);
					NavManager.NavigateTo($"/Warehouse", true);
				}


			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
