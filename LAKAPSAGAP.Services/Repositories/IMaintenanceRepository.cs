using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Repositories
{
	public interface IMaintenanceRepository
	{
		public Task<StockType> CreateStockType(string Name);
		public Task<StockCategory> CreateStockCategory(string Name);
		public Task<StockItem> CreateStockItem(string Name);
		public Task<UoM> CreateUoM(string Name);
		public Task<Floor>  CreateFloor(string Name,string WarehouseId);
		public Task<Rack> CreateRack(string Name);
	}
}
