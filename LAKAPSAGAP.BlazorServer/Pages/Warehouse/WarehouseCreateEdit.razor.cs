
namespace LAKAPSAGAP.BlazorServer.Pages.Warehouse
{
	public partial class WarehouseCreateEdit
	{
		public WarehouseViewModel model { get; set; } = new();
		public List<WarehouseViewModel> warehouses { get; set; } = new();

		private string _selectedWhse = String.Empty;

		private bool _isCreating = true;

		protected override Task OnInitializedAsync()
		{
			warehouses = new List<WarehouseViewModel>
			{
				new WarehouseViewModel { Id = "CREATE", Name = "Add New Warehouse" },
				new WarehouseViewModel { Id = "Whse01", Name = "Warehouse 1", Location = "Location 1" },
				new WarehouseViewModel { Id = "Whse02", Name = "Warehouse 2", Location = "Location 2" },
				new WarehouseViewModel { Id = "Whse03", Name = "Warehouse 3", Location = "Location 3" },
			};

			return base.OnInitializedAsync();
		}

		private void CreateOrEdit(string value)
		{
			if (value == "CREATE")
			{
				_isCreating = true;
				model = new();
			}
			else if (value is string && warehouses.Any(x => x.Id == value))
			{
				model = warehouses.Single(x => x.Id == value);
                _isCreating = false;
			}
			else
			{
				_isCreating = false;
			}

        }


	}
}
