namespace LAKAPSAGAP.Services.Repositories;

public interface IStockItemRepository
{
	Task<string?> CreateStockItem(StockItemViewModel stockItemVM);
	Task<StockItem?> GetStockItem(string stockItemID);
	Task<List<StockItem>> GetAllStockItem();
	Task<bool> UpdateStockItem(StockItemViewModel stockItemVM);
}
