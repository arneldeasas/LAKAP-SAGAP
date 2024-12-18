using LAKAPSAGAP.Models.Models;
using LAKAPSAGAP.Services.Core.Helpers;

namespace LAKAPSAGAP.Services.Core;

public class StockItemRepository(MyDbContext context) : IStockItemRepository
{
	readonly MyDbContext _context = context;

	public async Task<string?> CreateStockItem(StockItemViewModel stockItemVM)
	{

		try
		{
			int count = await _context.GetCount<StockItem>();
			string Id = IdGenerator.GenerateId(IdGenerator.PFX_STOCKITEM, count);

			var newStockItem = await _context.Create<StockItem>(new()
			{
				Id = Id,
				Name = stockItemVM.Name,
				CategoryId = stockItemVM.CategoryId,
				UoMId = stockItemVM.UoMId,
				isArchived = stockItemVM.isArchived
			});

			return newStockItem.Id;
		}
		catch (Exception)
		{
			return null;
		}

	}

	public async Task<List<StockItem>> GetAllStockItem()
	{
		try
		{
			List<StockItem> stockItemList = [];
			stockItemList = await _context.GetAllNotDeleted<StockItem>();
			return stockItemList;
		}
		catch (Exception)
		{
			return [];
		}
	}
	public async Task<StockItem?> GetStockItem(string stockItemID)
	{
		try
		{
			StockItem? stockItem = null;
			stockItem = await _context.GetByIdIncludeArchivedsOnly<StockItem>(stockItemID);
			return stockItem;
		}
		catch (Exception)
		{
			return null;
		}
	}
	public async Task<bool> UpdateStockItem(StockItemViewModel stockItemVM)
	{
		try
		{
			StockItem? stockItem = await _context.GetByIdIncludeArchivedsOnly<StockItem>(stockItemVM.Id);
			if (stockItem == null) return false;

			stockItem.Name = stockItemVM.Name;
			stockItem.CategoryId = stockItemVM.CategoryId;
			stockItem.UoMId = stockItemVM.UoMId;
			stockItem.isArchived = stockItemVM.isArchived;

			await _context.UpdateItem<StockItem>(stockItem);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<bool> SoftDeleteStockItem(string stockItemId)
	{
		try
		{
			StockItem? stockItem = await _context.GetByIdIncludeArchivedsOnly<StockItem>(stockItemId);
			if (stockItem == null) return false;

			stockItem.IsDeleted = true;

			await _context.UpdateItem<StockItem>(stockItem);


			// TODO: Soft delete also all parents of this stock item

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<List<StockItem>> GetAllActiveStockItem()
	{
		try
		{
			List<StockItem> stockItemList = [];
			stockItemList = await _context.GetAllActive<StockItem>();
			return stockItemList;
		}
		catch (Exception)
		{
			return [];
		}
	}

	public async Task<List<StockItemViewModel>> GetStocks()
	{
		try
		{
			List<StockItem> stocks = [];
			stocks = await _context.StockItems
								.Include(x => x.StockDetailList)
								.Include(x => x.Category)
								.Include(x => x.UoM)
								.Where(x => x.isArchived != true && x.IsDeleted != true && x.StockDetailList.Count > 0)
								.ToListAsync();

			

			List<StockItemViewModel> stocksVM = stocks.Select(x => new StockItemViewModel
			{
				Id= x.Id,
				Name = x.Name,
				CategoryId = x.CategoryId,
				CategoryName = x.Category.Name,
				
				UoMId = x.UoMId,
				UoMName = x.UoM.Name,
				
			}).ToList();


			foreach(var stockItem in stocksVM)
			{
				//process first the quantity from kits
				//List<ReliefSent> reliefSentList = await _context.ReliefSents
				//	.Include(x=>x.ReliefRequest)
				//	.Include(x=>x.SentKitList)
				//	.ThenInclude(x=> x.Kit) 
				//	.ThenInclude(x => x.KitComponentList)
				//	.ToListAsync();
				int totalSentQuantity = 0;

                List<ReliefSentKit> reliefSentKitList =  await _context.ReliefSentKits
					.Include(x=>x.ReliefSent)
					.ThenInclude(x=>x.ReliefRequest)
                    .Include(x=>x.Kit)
					.ThenInclude(x=>x.KitComponentList)
					.Where(x=>x.Kit.KitComponentList.Any(x=>x.StockItemId == stockItem.Id))
					.ToListAsync();

				foreach (var reliefSentKit in reliefSentKitList)
				{
					string reliefRequestId = reliefSentKit.ReliefSent.ReliefRequestId;
					ReliefRequestDetail reliefRequest = await _context.ReliefRequests.Where(x=>x.Id == reliefRequestId ).FirstOrDefaultAsync();

					int multiplier = reliefRequest.NumberOfRecipients;
					
                    int QuantityOfStockItemInKit = reliefSentKit.Kit.KitComponentList.Where(x => x.StockItemId == stockItem.Id).Sum(x => x.Quantity);
                    totalSentQuantity += (QuantityOfStockItemInKit * multiplier);
                }

				List<ReliefSentItem> reliefSentItemList = await _context.ReliefSentItems
                    .Include(x => x.ReliefSent)
                    .ThenInclude(x => x.ReliefRequest)
                    .Where(x => x.StockItemId == stockItem.Id)
                    .ToListAsync();

                foreach (var reliefSentItem in reliefSentItemList)
				{
                    string reliefRequestId = reliefSentItem.ReliefSent.ReliefRequestId;
                    ReliefRequestDetail reliefRequest = await _context.ReliefRequests.Where(x => x.Id == reliefRequestId).FirstOrDefaultAsync();

                    int multiplier = reliefRequest.NumberOfRecipients;

                    totalSentQuantity += (reliefSentItem.Quantity * multiplier);
                }


                    int index = stocksVM.FindIndex(x => x.Id == stockItem.Id);
                stockItem.ReceivedQty = stocks[index].StockDetailList.Sum(x => x.Quantity);
                stockItem.SentQty = totalSentQuantity;
				stockItem.StockQty = stockItem.ReceivedQty - stockItem.SentQty;

                //  int multiplier = reliefSentKitList.Count();

                //int QuantityOfStockItemInKit = reliefSentKitList.Count() > 0 ? reliefSentKitList.Sum(x => x.Kit.KitComponentList.Where(y => y.StockItemId == stockItem.Id).Sum(y => y.Quantity)) : 0;

            }


			return stocksVM;
		}
		catch (Exception)
		{
			throw;
		}
	}
}
