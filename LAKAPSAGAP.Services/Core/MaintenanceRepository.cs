
using LAKAPSAGAP.Services.Core.Helpers;

namespace LAKAPSAGAP.Services.Core
{
	public class MaintenanceRepository : IMaintenanceRepository
	{
		readonly MyDbContext _context;
		public MaintenanceRepository(MyDbContext context)
		{
			_context = context;
		}

		public async Task<StockCategory> CreateStockCategory(StockCategoryViewModel stockCategoryViewModel)
		{
			try
			{
				int count = await _context.GetCount<StockCategory>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_STOCKCATEGORY, count);

				var newStockCategory = new StockCategory
				{
					Id = Id,
					Name = stockCategoryViewModel.Name.Trim(),

				};
				return await _context.Create<StockCategory>(newStockCategory);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<StockCategory> UpdateStockCategory(StockCategoryViewModel stockCategoryViewModel)
		{
			try
			{
				var updatedStockCategory = new StockCategory
				{
					Name = stockCategoryViewModel.Name.Trim()
				};
				return await _context.UpdateItem<StockCategory>(updatedStockCategory);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<StockCategory> DeleteStockCategory(string Id)
		{
			try
			{
				var stockCategory = await _context.GetById<StockCategory>(Id);
				if (stockCategory is null)
				{
					throw new Exception("Record not found.");
				}
				stockCategory.IsDeleted = true;
				return await _context.UpdateItem<StockCategory>(stockCategory);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<StockCategory> ArchiveStockCategory(string Id)
		{
			try
			{
				var stockCategory = await _context.GetById<StockCategory>(Id);
				if (stockCategory is null)
				{
					throw new Exception("Record not found.");
				}
				stockCategory.isArchived = true;
				return await _context.UpdateItem<StockCategory>(stockCategory);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<StockCategory> GetStockCategoryById(string Id)
		{
			try
			{
				return await _context.GetById<StockCategory>(Id);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<List<StockCategory>> GetAllStockCategories()
		{
			try
			{
				return await _context.GetAll<StockCategory>();
			}
			catch (Exception)
			{

				throw;
			}
		}


		public async Task<StockItem> CreateStockItem(StockItemViewModel stockItemViewModel)
		{
			try
			{
				int count = await _context.GetCount<StockItem>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_STOCKITEM, count);
				var newStockItem = new StockItem
				{
					Id = Id,
			
					StockCategoryId = stockItemViewModel.StockCategoryId,
					Name = stockItemViewModel.Name.Trim(),
					UoMId = stockItemViewModel.UoMId
				};
				return await _context.Create<StockItem>(newStockItem);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<StockItem> UpdateStockItem(StockItemViewModel stockItemViewModel)
		{
			try
			{
				var updatedStockItem = new StockItem
				{
					UoMId = stockItemViewModel.UoMId,
					StockCategoryId = stockItemViewModel.StockCategoryId,
					Name = stockItemViewModel.Name.Trim(),
				};
					
				return	await _context.UpdateItem<StockItem>(updatedStockItem);


			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<StockItem> DeleteStockItem(string Id)
		{
			try
			{
				var stockItem = await _context.GetById<StockItem>(Id);
				if (stockItem is null)
				{
					throw new Exception("Record not found.");
				}
				stockItem.IsDeleted = true;
				return await _context.UpdateItem<StockItem>(stockItem);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<StockItem> ArchiveStockItem(string Id)
		{
			try
			{
				var stockItem = await _context.GetById<StockItem>(Id);
				if(stockItem is null)
				{
					throw new Exception("Record not found.");
				}
				stockItem.isArchived = true;
				return await _context.UpdateItem<StockItem>(stockItem);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<StockItem> GetStockItemById(string Id)
		{
			try
			{
				return await _context.GetById<StockItem>(Id);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<List<StockItem>> GetAllStockItems()
		{
			try
			{
				return await _context.GetAll<StockItem>();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<UoM> CreateUoM(UoMViewModel uomViewModel)
		{
			try
			{
				int count = await _context.GetCount<UoM>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_UOM, count);
				var newUoM = new UoM
				{
					Id = Id,
					Name = uomViewModel.Name.Trim(),
				};
				return await _context.Create<UoM>(newUoM);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<UoM> UpdateUoM(UoMViewModel uomViewModel)
		{
			try
			{
				var updatedUoM = new UoM
				{
					Name = uomViewModel.Name.Trim()
				};
				return await _context.UpdateItem<UoM>(updatedUoM);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<UoM> DeleteUoM(string Id)
		{
			try
			{
				var uom = await _context.GetById<UoM>(Id);
				if (uom is null)
				{
					throw new Exception("Record not found.");
				}
				uom.IsDeleted = true;
				return await _context.UpdateItem<UoM>(uom);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<UoM> ArchiveUoM(string Id)
		{
			try
			{
				var uom = await _context.GetById<UoM>(Id);
				if (uom is null)
				{
					throw new Exception("Record not found.");
				}
				uom.isArchived = true;
				return await _context.UpdateItem<UoM>(uom);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<UoM> GetUoMById(string Id)
		{
			try
			{
				return await _context.GetById<UoM>(Id);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<List<UoM>> GetAllUoMs()
		{
			try
			{
				return await _context.GetAll<UoM>();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<Floor> CreateFloor(FloorViewModel floorViewModel)
		{
			try
			{
				int count = await _context.GetCount<Floor>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_FLOOR, count);
				var newFloor = new Floor
				{
					Id = Id,
					Name = floorViewModel.Name.Trim(),
				};
				return await _context.Create<Floor>(newFloor);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Floor> UpdateFloor(FloorViewModel floorViewModel)
		{
			try
			{
				var updatedFloor = new Floor
				{
					Name = floorViewModel.Name.Trim()
				};
				return await _context.UpdateItem<Floor>(updatedFloor);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Floor> DeleteFloor(string Id)
		{
			try
			{
				var floor = await _context.GetById<Floor>(Id);
				if (floor is null)
				{
					throw new Exception("Record not found.");
				}
				floor.IsDeleted = true;
				return await _context.UpdateItem<Floor>(floor);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Floor> ArchiveFloor(string Id)
		{
			try
			{
				var floor = await _context.GetById<Floor>(Id);
				if (floor is null)
				{
					throw new Exception("Record not found.");
				}
				floor.isArchived = true;
				return await _context.UpdateItem<Floor>(floor);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Floor> GetFloorById(string Id)
		{
			try
			{
				return await _context.GetById<Floor>(Id);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<List<Floor>> GetAllFloors()
		{
			try
			{
				return await _context.GetAll<Floor>();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<Rack> CreateRack(RackViewModel rackViewModel)
		{
			try
			{
				int count = await _context.GetCount<Rack>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_RACK, count);
				var newRack = new Rack
				{
					Id = Id,
					Name = rackViewModel.Name.Trim(),
					FloorId = rackViewModel.FloorId
				};
				return await _context.Create<Rack>(newRack);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Rack> UpdateRack(RackViewModel rackViewModel)
		{
			try
			{
				var updatedRack = new Rack
				{
					Name = rackViewModel.Name.Trim(),
					FloorId = rackViewModel.FloorId
				};
				return await _context.UpdateItem<Rack>(updatedRack);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Rack> DeleteRack(string Id)
		{
			try
			{
				var rack = await _context.GetById<Rack>(Id);
				if (rack is null)
				{
					throw new Exception("Record not found.");
				}
				rack.IsDeleted = true;
				return await _context.UpdateItem<Rack>(rack);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Rack> ArchiveRack(string Id)
		{
			try
			{
				var rack = await _context.GetById<Rack>(Id);
				if (rack is null)
				{
					throw new Exception("Record not found.");
				}
				rack.isArchived = true;
				return await _context.UpdateItem<Rack>(rack);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<Rack> GetRackById(string Id)
		{
			try
			{
				return await _context.GetById<Rack>(Id);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<List<Rack>> GetAllRacks()
		{
			try
			{
				return await _context.GetAll<Rack>();
			}
			catch (Exception)
			{

				throw;
			}
		}


	} }
