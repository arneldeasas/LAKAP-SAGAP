using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAKAPSAGAP.Models.ViewModel;
using LAKAPSAGAP.Services.Core.Helpers;

namespace LAKAPSAGAP.Services.Core
{
	public class ReliefReceivedRepository:IReliefReceivedRepository
	{
		readonly MyDbContext _context;
		ReliefReceivedRepository(MyDbContext context)
		{
			_context = context;
		}
		public async Task<ReliefReceived> CreateReliefReceived(ReliefReceivedViewModel reliefReceivedViewModel)
		{
			try
			{
				int count = await _context.GetCount<ReliefReceived>();
				string Id = IdGenerator.GenerateId(IdGenerator.PFX_RELIEFRECEIVED,count);

				var newReliefReceived = new ReliefReceived
				{
					Id = Id,
					ReliefType = reliefReceivedViewModel.ReliefType,
					ReceivedBy = reliefReceivedViewModel.ReceivedBy,
					ReceivedFrom = reliefReceivedViewModel.ReceivedFrom,
					TruckPlateNumber = reliefReceivedViewModel.TruckPlateNumber,
					DriverName = reliefReceivedViewModel.DriverName,
					ReceivedDate = reliefReceivedViewModel.ReceivedDate,
				};

				newReliefReceived = await _context.Create<ReliefReceived>(newReliefReceived);

				return newReliefReceived;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<ReliefReceived> UpdateReliefReceived(ReliefReceivedViewModel reliefReceivedViewModel)
		{
			try
			{
				var updateReliefReceived = new ReliefReceived
				{
					ReliefType = reliefReceivedViewModel.ReliefType,
					ReceivedBy = reliefReceivedViewModel.ReceivedBy,
					ReceivedFrom = reliefReceivedViewModel.ReceivedFrom,
					TruckPlateNumber = reliefReceivedViewModel.TruckPlateNumber,
					DriverName = reliefReceivedViewModel.DriverName,
					ReceivedDate = reliefReceivedViewModel.ReceivedDate
				};

				updateReliefReceived = await _context.UpdateItem<ReliefReceived>(updateReliefReceived);

				return updateReliefReceived;	
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<ReliefReceived> DeleteReliefReceived(string Id)
		{
			try
			{
				var reliefReceived = await _context.GetById<ReliefReceived>(Id);
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

		public async Task<ReliefReceivedViewModel> GetAllInitialSelectionOptions (ReliefReceivedViewModel reliefReceivedViewModel)
		{
			Task<List<StockType>> StockTypeList = _context.GetAll<StockType>();
			Task<List<StockCategory>> StockCategoryList = _context.GetAll<StockCategory>();
			Task<List<UoM>> UoMList = _context.GetAll<UoM>();
			Task<List<Floor>> FloorList = _context.GetAll<Floor>();

			await Task.WhenAll(StockTypeList, StockCategoryList, UoMList, FloorList);

			reliefReceivedViewModel.ReliefReceivedFormSelections.StockTypeList = await StockTypeList;
			reliefReceivedViewModel.ReliefReceivedFormSelections.StockCategoryList = await StockCategoryList;
			reliefReceivedViewModel.ReliefReceivedFormSelections.UoMList = await UoMList;
			reliefReceivedViewModel.ReliefReceivedFormSelections.FloorList = await FloorList;

			return reliefReceivedViewModel;

		}

		public async Task<List<StockItem>> GetAllStockItemBasedOnTypeAndCategory(string stockTypeId, string stockCategoryId)
		{
			try
			{
				var stockItemList = await _context.StockItem.WhereIsNotArchivedAndDeleted().Where(x=>x.StockTypeId == stockTypeId && x.StockCategoryId == stockCategoryId).ToListAsync();	

				return stockItemList;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<List<Rack>> GetAllRacksBasedOnFloor(string floorId)
		{
			try
			{
				var rackList = await _context.GetAll<Rack>();
				return rackList;
			}
			catch (Exception)
			{

				throw;
			}
		} 

	}
}
