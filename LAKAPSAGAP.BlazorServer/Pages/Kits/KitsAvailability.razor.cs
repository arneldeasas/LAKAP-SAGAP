using LAKAPSAGAP.Services;
using LAKAPSAGAP.Services.Core;
using Microsoft.EntityFrameworkCore;
using static LAKAPSAGAP.Models.Models.Kit;

namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsAvailability
{
	[Inject] IKittingRepository KittingRepo { get; set; }

	[Parameter]
	public List<KitViewModel> KitList { get; set; } = new List<KitViewModel>();
	public List<StockItem> stockItems = new List<StockItem>();
	private bool isLoading = false;
	protected override async Task OnInitializedAsync()
	{
		//await LoadAllKits();
	}
	
	
}
