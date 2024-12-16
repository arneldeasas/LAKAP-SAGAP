using LAKAPSAGAP.Services.Core.Helpers;

namespace LAKAPSAGAP.Services.Core;

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
				int countRck = await _context.GetCount<Rack>();
				foreach (var (floorViewModel, index) in warehouseViewModel.FloorList.Select((value, i) => (value, i)))
				{
					//countFlr += 1;
					string flrId = IdGenerator.GenerateId(IdGenerator.PFX_FLOOR, countFlr);

					var floor = new Floor
					{
						Id = flrId,
						WarehouseId = whseId,
						Name = floorViewModel.Name,
					};

					floor = await _context.Create<Floor>(floor);



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
						;
						countRck += 1;
					}
					countFlr += 1;
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
			var warehouse = await _context.Warehouses.FindAsync(warehouseViewModel.Id);
			warehouse.Name = warehouseViewModel.Name;
			warehouse.Location = warehouseViewModel.Location;


			await _context.SaveChangesAsync();

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

	public async Task<Warehouse?> GetWarehouseById(string Id)
	{
		try
		{
			var warehouse = await _context.Warehouses
									.AsNoTracking()
									.Where(x => x.Id == Id)
									.SingleOrDefaultAsync();

			if (warehouse == null) return null;

			var floors = await _context.Floors
								.AsNoTracking()
								.Where(x => x.WarehouseId == Id)
								.ToListAsync();
			foreach (var floor in floors)
			{
				var racks = await _context.Racks
									.AsNoTracking()
									.Where(x => x.FloorId == floor.Id)
									.ToListAsync();

				floor.Racks = racks;
			}

			warehouse.FloorList = floors;

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

	async Task<Warehouse> IWarehouseRepository.PickWarehouse()
	{
		try
		{
			var warehouse = await _context.Warehouses
								.OrderBy(x => x.DateCreated)
								.FirstOrDefaultAsync();

			return warehouse;
		}
		catch (Exception)
		{

			throw;
		}
	}
}
