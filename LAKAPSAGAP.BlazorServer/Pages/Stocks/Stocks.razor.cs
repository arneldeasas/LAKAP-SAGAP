namespace LAKAPSAGAP.BlazorServer.Pages.Stocks
{
    public partial class Stocks
    {
        private List<BreadcrumbViewModel> Breadcrumbs = new()
        {
            new BreadcrumbViewModel { Path = "javascript:void(0);", Text = "Warehouse" },
            new BreadcrumbViewModel { Path = "/Warehouse/Stocks", Text = "Stocks" },
        };
    }
}
