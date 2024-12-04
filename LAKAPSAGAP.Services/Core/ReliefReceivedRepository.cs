using LAKAPSAGAP.Models.ViewModel;
using LAKAPSAGAP.Services.Core.Helpers;

namespace LAKAPSAGAP.Services.Core;

public class ReliefReceivedRepository : IReliefReceivedRepository
{
	readonly MyDbContext _context;
	public ReliefReceivedRepository(MyDbContext context)
	{
		_context = context;
	}
	public async Task<ReliefReceived> CreateReliefReceived(ReliefReceived reliefReceived)
	{
		using var transaction = _context.Database.BeginTransaction();
		try
		{
			int count = await _context.GetCount<ReliefReceived>();
			string Id = IdGenerator.GenerateId(IdGenerator.PFX_RELIEFRECEIVED, count);
			reliefReceived.Id = Id;

			int stockDetailCount = await _context.GetCount<StockDetail>();
			List<StockDetail> stockDetailList = reliefReceived.StockDetailList.Select((x, index) =>
			{
				stockDetailCount += index;
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_STOCKDETAIL, stockDetailCount);
				x.BatchNo = reliefReceived.Id;
				x.Id = Id;
				return x;
			}).ToList();

			if (stockDetailList.Count == 0) throw new Exception("No items");
			reliefReceived.StockDetailList = stockDetailList;
			ReliefReceived newReliefReceived = null;
			newReliefReceived = await _context.Create(reliefReceived);
			//stockDetailList = await _context.CreateMany(stockDetailList);

			await transaction.CommitAsync();
			return newReliefReceived;
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
	}

	public async Task<bool> UpdateReliefReceived(ReliefReceived reliefReceived)
	{
		try
		{
			var toUpdateReliefReceived = await _context.GetById<ReliefReceived>(reliefReceived.Id);
			if (toUpdateReliefReceived is null) return false;

			toUpdateReliefReceived.AcquisitionType = reliefReceived.AcquisitionType;
			toUpdateReliefReceived.ReceivedBy = reliefReceived.ReceivedBy;
			toUpdateReliefReceived.ReceivedFrom = reliefReceived.ReceivedFrom;
			toUpdateReliefReceived.PlateNo = reliefReceived.PlateNo;
			toUpdateReliefReceived.DriverName = reliefReceived.DriverName;
			toUpdateReliefReceived.ReceivedDate = reliefReceived.ReceivedDate;

			await _context.UpdateItem(toUpdateReliefReceived);

			return true;
		}
		catch (Exception)
		{

			return false;
		}
	}

	public async Task<ReliefReceived> DeleteReliefReceived(string Id)
	{
		try
		{
			var reliefReceived = await _context.GetById<ReliefReceived>(Id);
			if (reliefReceived is null)
			{
				throw new Exception("Record not found");
			}
			reliefReceived.IsDeleted = true;

			bool success = await UpdateReliefReceived(reliefReceived);
			if (!success) throw new Exception("Failed to update");

			return reliefReceived;
		}
		catch (Exception)
		{

			throw;
		}

	}

	public async Task<ReliefReceived> ArchiveReliefReceived(string Id)
	{
		try
		{
			var reliefReceived = await _context.GetById<ReliefReceived>(Id);
			if (reliefReceived is null)
			{
				throw new Exception("Record not found");
			}
			reliefReceived.isArchived = true;

			bool success = await UpdateReliefReceived(reliefReceived);
			if (!success) throw new Exception("Failed to update");

			return reliefReceived;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<List<ReliefReceived>> GetAllReliefReceived()
	{
		try
		{
			var reliefReceivedList = await _context.GetAll<ReliefReceived>();

			return reliefReceivedList;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<ReliefReceivedFormSelections> GetAllInitialSelectionOptions(ReliefReceivedViewModel reliefReceivedViewModel)
	{
		try
		{


			Task<List<Floor>> FloorList = _context.GetAll<Floor>();

			await Task.WhenAll(FloorList);


			reliefReceivedViewModel.ReliefReceivedFormSelections.FloorList = await FloorList;

			return reliefReceivedViewModel.ReliefReceivedFormSelections;
		}
		catch (Exception)
		{

			throw;
		}

	}

	public async Task<List<Floor>> GetAllFloorsActive()
	{
		List<Floor> floors = [];
		try
		{
			floors = await _context.GetAllActive<Floor>();
			return floors;
		}
		catch (Exception)
		{

			return floors;
		}
	}
	public async Task<List<Rack>> GetAllRacksBasedOnFloor(string floorId)
	{
		try
		{
			var rackList = await _context.Racks.WhereIsNotArchivedAndDeleted().Where(x => x.FloorId == floorId).ToListAsync();
			return rackList;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public async Task<List<Floor>> GetAllFloorsActiveBasedOnWarehouse(string whseId)
	{
		try
		{
			var floorList = await _context.Floors.WhereIsNotArchivedAndDeleted().Where(x => x.WarehouseId == whseId).ToListAsync();
			return floorList;
		}
		catch (Exception)
		{

			throw;
		}
	}
}
