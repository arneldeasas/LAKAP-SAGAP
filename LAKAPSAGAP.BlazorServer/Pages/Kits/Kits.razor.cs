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
			//await LoadAllKits();
			//base.OnInitializedAsync();
		}
		protected override void OnParametersSet()
		{
            if (id is null)
            {
				Breadcrumbs.Add(new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Kits", Text = "Kits" });

			}

			Breadcrumbs.Add(new BreadcrumbViewModel { Path = $@"/Warehouse/{id}/Kits", Text = "Kits" });
		}
		
	}
}
