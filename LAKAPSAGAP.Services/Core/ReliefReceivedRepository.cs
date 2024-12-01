using LAKAPSAGAP.Models.Models;
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
	public async Task<ReliefReceived> CreateReliefReceived(ReliefReceivedViewModel reliefReceivedViewModel)
	{
		using var transaction = _context.Database.BeginTransaction();
		try
		{
			int count = await _context.GetCount<ReliefReceived>();
			string Id = IdGenerator.GenerateId(IdGenerator.PFX_RELIEFRECEIVED, count);

			var newReliefReceived = new ReliefReceived
			{
				Id = Id,
				ReliefType = reliefReceivedViewModel.ReliefType,
				ReceivedBy = reliefReceivedViewModel.ReceivedBy,
				ReceivedFrom = reliefReceivedViewModel.ReceivedFrom,
				TruckPlateNumber = reliefReceivedViewModel.TruckPlateNumber,
				DriverName = reliefReceivedViewModel.DriverName,
				DateReceived = reliefReceivedViewModel.ReceivedDate,
			};

			newReliefReceived = await _context.Create<ReliefReceived>(newReliefReceived);

			int stockDetailCount = await _context.GetCount<StockDetail>();


			List<StockDetail> stockDetailList = reliefReceivedViewModel.StockDetailViewList.Select((x, index) =>
			{
				stockDetailCount += index;
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_STOCKDETAIL, stockDetailCount);
				return new StockDetail
				{
					Id = Id,
					BatchNumber = newReliefReceived.Id,
					ItemId = x.ItemId,
					Quantity = x.Quantity,
					RackId = x.RackId,
					DateExpiry = x.ExpiryDate
				};
			}).ToList();

			stockDetailList = await _context.CreateMany<StockDetail>(stockDetailList);
			newReliefReceived.StockDetailList = stockDetailList;

			await transaction.CommitAsync();
			return newReliefReceived;
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
	}

	public async Task<bool> UpdateReliefReceived(ReliefReceivedViewModel reliefReceivedVM)
	{
		try
		{
			var reliefReceived = await _context.GetById<ReliefReceived>(reliefReceivedVM.Id);
			if (reliefReceived is null) return false;

			reliefReceived.ReliefType = reliefReceivedVM.ReliefType;
			reliefReceived.ReceivedBy = reliefReceivedVM.ReceivedBy;
			reliefReceived.ReceivedFrom = reliefReceived.ReceivedFrom;
			reliefReceived.TruckPlateNumber = reliefReceived.TruckPlateNumber;
			reliefReceived.DriverName = reliefReceived.DriverName;
			reliefReceived.DateReceived = reliefReceived.DateReceived;

			await _context.UpdateItem<ReliefReceived>(reliefReceived);

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
			floors = await _context.GetAll<Floor>();
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
			var rackList = await _context.Racks.WhereIsNotArchivedAndDeleted().Where(x=>x.FloorId == floorId).ToListAsync();
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
