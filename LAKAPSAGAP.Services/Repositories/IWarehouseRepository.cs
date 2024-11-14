using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAKAPSAGAP.Models.ViewModel;

namespace LAKAPSAGAP.Services.Repositories
{
	public interface IWarehouseRepository
	{
		Task<Warehouse> CreateWarehouse(WarehouseViewModel warehouseViewModel);
		Task<Warehouse> UpdateWarehouse(WarehouseViewModel warehouseViewModel);
		Task<Warehouse> DeleteWarehouse(string Id);
		Task<Warehouse> ArchiveWarehouse(string Id);
		Task<Warehouse?> GetWarehouseById (string Id);
		Task<List<Warehouse>> GetAllWarehouses();
		Task<Warehouse> PickWarehouse();
	}
}
