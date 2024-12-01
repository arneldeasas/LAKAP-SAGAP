namespace LAKAPSAGAP.BlazorServer.Pages.Kits
{
	public partial class Kits
	{
		[Parameter] public string? id { get; set; }
		[Inject] DialogService _dialogService { get; set; }

		int currentTab = 0;

		private List<BreadcrumbViewModel> Breadcrumbs = new()
		{
			new BreadcrumbViewModel { Path = "/Warehouse", Text = "Warehouse" },
		};

		
		protected override void OnParametersSet()
		{
            if (id is null)
            {
				Breadcrumbs.Add(new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Kits", Text = "Stocks" });

			}

			Breadcrumbs.Add(new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Kits", Text = "Stocks" });
		}
	}
}
