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
		public Task<Warehouse> CreateWarehouse(WarehouseViewModel warehouseViewModel);
		public Task<Warehouse> UpdateWarehouse(WarehouseViewModel warehouseViewModel);
		public Task<Warehouse> DeleteWarehouse(string Id);
		public Task<Warehouse> ArchiveWarehouse(string Id);
		public Task<Warehouse> GetWarehouseById (string Id);
		public Task<List<Warehouse>> GetAllWarehouses();
	}
}
