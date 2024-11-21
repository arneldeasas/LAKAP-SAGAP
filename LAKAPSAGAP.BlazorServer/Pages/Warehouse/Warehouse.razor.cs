
namespace LAKAPSAGAP.BlazorServer.Pages.Warehouse
{
	public partial class Warehouse
	{
		[Parameter] required public string Id { get; set; }
		[Inject] DialogService _dialogService { get; set; }
		[Inject] IWarehouseRepository? WarehouseRepo { get; set; }
		[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
		[Inject] NavigationManager? NavManager { get; set; }



		bool Loading = true;
		int retries = 0;

		public WarehouseViewModel model { get; set; } = new();
		public List<LAKAPSAGAP.Models.Models.Warehouse> warehouses { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
			
        }

        protected override async Task OnParametersSetAsync()
		{

            var res = await WarehouseRepo.GetWarehouseById(Id);

            if (res is null)
            {
				res = await WarehouseRepo.PickWarehouse();
				if (res is null) NavManager.NavigateTo("/Warehouse");
				NavManager.NavigateTo($@"/Warehouse/{res.Id}/Stocks");
			}

            model = new WarehouseViewModel
			{
				Id = res.Id,
				Name = res.Name,
				Location = res.Location
			};


			foreach (var (floor, index) in res.FloorList.Select((floor, index) => (floor, index)))
			{
				model.FloorList.Add(new FloorViewModel
				{
					Name = floor.Name,
				});

				foreach (var rack in floor.Racks)
				{
					model.FloorList[index].RackList.Add(new RackViewModel
					{
						FloorId = floor.Id,
						Name = rack.Name
					});
				}
			}

			warehouses = await WarehouseRepo.GetAllWarehouses();
			Loading = false;
		}

		public void NavigateToWhse(string selectedWhse)
		{
			NavManager.NavigateTo($"/Warehouse/{selectedWhse}", true);
		}

	}
}

