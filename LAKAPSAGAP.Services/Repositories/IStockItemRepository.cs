using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Repositories
{
	public interface IStockItemRepository
	{
		Task<string?> CreateStockItem(StockItemViewModel stockItemVM);
		Task<StockItem?> GetStockItem(string stockItemID);
		Task<List<StockItem>> GetAllStockItem();
		Task<bool> UpdateStockItem(StockItemViewModel stockItemVM);
	}
}
