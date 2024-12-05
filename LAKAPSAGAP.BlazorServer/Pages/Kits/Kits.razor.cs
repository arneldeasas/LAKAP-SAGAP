namespace LAKAPSAGAP.BlazorServer.Pages.Kits
{
	public partial class Kits
	{
		[Parameter] public string? id { get; set; }
	
		[Inject] DialogService _dialogService { get; set; }
		[Inject] IKittingRepository KittingRepo { get; set; }
		public List<KitViewModel> KitList { get; set; } = new List<KitViewModel>();
		int currentTab = 0;

		private List<BreadcrumbViewModel> Breadcrumbs = new()
		{
			new BreadcrumbViewModel { Path = "/Warehouse", Text = "Warehouse" },
		};

		protected override async Task OnInitializedAsync()
		{
			await LoadAllKits();
			base.OnInitializedAsync();
		}
		protected override void OnParametersSet()
		{
            if (id is null)
            {
				Breadcrumbs.Add(new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Kits", Text = "Stocks" });

			}

			Breadcrumbs.Add(new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Kits", Text = "Stocks" });
		}
		async Task LoadAllKits()
		{
			try
			{
			
				List<Kit> kitList = await KittingRepo.GetAllKitsAsync();
				KitList = kitList.Select(x => new KitViewModel
				{
					Id = x.Id,
					Name = x.Name,
					Description = x.Description,
					KitType = x.KitType,
					KitComponentList = x.KitComponentList.Select(y => new KitComponentViewModel
					{
						Id = y.Id,
						ItemName = y.StockItem.Name,
						Quantity = y.Quantity
					}).ToList()
				}).ToList();
			}
			catch (Exception)
			{

				throw;
			}
			
		}
	}
}
