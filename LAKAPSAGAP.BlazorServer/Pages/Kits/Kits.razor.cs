namespace LAKAPSAGAP.BlazorServer.Pages.Kits
{
	public partial class Kits
	{
		[Parameter] public static string? id { get; set; }
		[Inject] DialogService _dialogService { get; set; }

		private List<BreadcrumbViewModel> Breadcrumbs = new()
		{
			new BreadcrumbViewModel { Path = "javascript:void(0);", Text = "Warehouse" },
			new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Kits", Text = "Kits" },
		};

	}
}
