using LAKAPSAGAP.BlazorServer.Pages.UserManagement;
using Radzen;

namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
	public partial class Stocks
	{
		[Parameter] public string? id { get; set; }
		[Inject] DialogService _dialogService { get; set; }

		private List<BreadcrumbViewModel> Breadcrumbs = new()
		{
			new BreadcrumbViewModel { Path = "/Warehouse", Text = "Warehouse" },
		};

		protected override void OnParametersSet()
		{
			//Breadcrumbs.Add(new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Stocks", Text = "Stocks" });
		}

	}
}
