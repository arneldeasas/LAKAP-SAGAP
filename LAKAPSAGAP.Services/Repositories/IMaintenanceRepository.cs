using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Repositories
{
	public interface IMaintenanceRepository
	{
		public Task<StockType> CreateStockType(StockTypeViewModel stockTypeViewModel);
		public Task<StockType> UpdateStockType(StockTypeViewModel stockTypeViewModel);
		public Task<StockType> DeleteStockType(string Id);
		public Task<StockType> ArchiveStockType(string Id);
		public Task<StockType> GetStockTypeById(string Id);
		public Task<List<StockType>> GetAllStockTypes();

		public Task<StockCategory> CreateStockCategory(string Name);
		public Task<StockItem> CreateStockItem(string Name);
		public Task<UoM> CreateUoM(string Name);
		public Task<Floor>  CreateFloor(string Name,string WarehouseId);
		public Task<Rack> CreateRack(string Name);
	}
}
