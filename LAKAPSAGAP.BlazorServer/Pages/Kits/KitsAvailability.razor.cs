using LAKAPSAGAP.Services;
using LAKAPSAGAP.Services.Core;
using Microsoft.EntityFrameworkCore;
using static LAKAPSAGAP.Models.Models.Kit;

namespace LAKAPSAGAP.BlazorServer.Pages.Kits;

public partial class KitsAvailability
{
	[Inject] IKittingRepository KittingRepo { get; set; }
	[Inject] MyDbContext _context { get; set; }
	public List<KitViewModel> KitList { get; set; } = new List<KitViewModel>();
	public List<StockItem> stockItems = new List<StockItem>();
	private bool isLoading = false;
	protected override async Task OnInitializedAsync()
	{
		await LoadAllKits();
	}
	
	async Task LoadAllKits()
	{
		try
		{
			if (isLoading) return;
			isLoading = true;
			List<Kit> kitList = await _context.Kits.WhereIsNotArchivedAndDeleted().Include(x=>x.KitComponentList).ThenInclude(x=>x.StockItem).ToListAsync();
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
		finally
		{
			isLoading = false;
		}
	}
}
