using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAKAPSAGAP.Models.ViewModel;
using LAKAPSAGAP.Services.Core.Helpers;

namespace LAKAPSAGAP.Services.Core
{
	public class WarehouseRepository : IWarehouseRepository
	{
		readonly MyDbContext _context;
		public WarehouseRepository(MyDbContext context) {
			_context = context;
		}
		public async Task<Warehouse> CreateWarehouse(WarehouseViewModel warehouseViewModel)
		{
			try
			{
				int count = await _context.GetCount<Warehouse>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_Warehouse, count);
				var newWarehouse = new Warehouse
				{
					Id = Id,
					Name = warehouseViewModel.Name.Trim(),
					Location = warehouseViewModel.Location.Trim(),
				};

				var warehouse = await _context.Create<Warehouse>(newWarehouse);
				return warehouse;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<Warehouse> UpdateWarehouse(WarehouseViewModel warehouseViewModel)
		{
			try
			{
				var updatedWarehouse = new Warehouse
				{
					Name = warehouseViewModel.Name.Trim(),
					Location = warehouseViewModel.Location.Trim(),
				};

				var warehouse = await _context.UpdateItem<Warehouse>(updatedWarehouse);

				return warehouse;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<Warehouse> DeleteWarehouse(string Id)
		{
			try
			{
				var warehouse = await _context.GetById<Warehouse>(Id);
				warehouse.IsDeleted = true;
				warehouse = await _context.UpdateItem<Warehouse>(warehouse);
				return warehouse;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<Warehouse> ArchiveWarehouse(string Id)
		{
			try
			{
				var warehouse = await _context.GetById<Warehouse>(Id);
				warehouse.isArchived = true;
				warehouse = await _context.UpdateItem(warehouse);
				return warehouse;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<Warehouse> GetWarehouseById(string Id)
		{
			try
			{
				var warehouse = await _context.GetById<Warehouse>(Id);
				return warehouse;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<List<Warehouse>> GetAllWarehouses()
		{
			try
			{
				List<Warehouse> warehouseList = await _context.GetAll<Warehouse>();
		
				return warehouseList;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
