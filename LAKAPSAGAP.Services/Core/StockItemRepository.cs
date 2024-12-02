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
}
