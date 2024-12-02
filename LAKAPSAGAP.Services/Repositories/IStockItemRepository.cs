namespace LAKAPSAGAP.Services.Repositories;

public interface IStockItemRepository
{
	Task<string?> CreateStockItem(StockItemViewModel stockItemVM);
	Task<StockItem?> GetStockItem(string stockItemID);
	Task<List<StockItem>> GetAllStockItem();
	Task<List<StockItem>> GetAllActiveStockItem();
	Task<bool> UpdateStockItem(StockItemViewModel stockItemVM);
	Task<bool> SoftDeleteStockItem(string stockItemId);
}
