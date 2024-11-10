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
		public WarehouseRepository(MyDbContext context)
		{
			_context = context;
		}
		public async Task<Warehouse> CreateWarehouse(WarehouseViewModel warehouseViewModel)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					int countWhse = await _context.GetCount<Warehouse>();
					string whseId = IdGenerator.GenerateId(IdGenerator.PFX_Warehouse, countWhse);

					var newWarehouse = new Warehouse
					{
						Id = whseId,
						Name = warehouseViewModel.Name.Trim(),
						Location = warehouseViewModel.Location.Trim(),
					};

					var warehouse = await _context.Create<Warehouse>(newWarehouse);
					int countFlr = await _context.GetCount<Floor>();

					foreach (var (floorViewModel, index) in warehouseViewModel.FloorList.Select((value, i) => (value, i)))
					{
						string flrId = IdGenerator.GenerateId(IdGenerator.PFX_FLOOR, countFlr);

						var floor = new Floor
						{
							Id = flrId,
							WarehouseId = whseId,
							Name = floorViewModel.Name,
						};

						floor = await _context.Create<Floor>(floor);

						int countRck = await _context.GetCount<Rack>();

						foreach (var (rack, rckIndex) in warehouseViewModel.FloorList[index].RackList.Select((value, i) => (value, i)))
						{
							string rckId = IdGenerator.GenerateId(IdGenerator.PFX_RACK, countRck);

							var createdRack = new Rack
							{
								Id = rckId,
								FloorId = floor.Id,
								Name = warehouseViewModel.FloorList[index].RackList[rckIndex].Name,
							};

							createdRack = await _context.Create<Rack>(createdRack);
							countRck += rckIndex;
						}
						countFlr += index;
					}

					transaction.Commit();
					return newWarehouse;
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}


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
				if (warehouse == null)
				{
					throw new Exception("Warehouse not found");
				}
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
				if (warehouse == null)
				{
					throw new Exception("Warehouse not found");
				}
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
