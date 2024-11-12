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
			using(var transaction = _context.Database.BeginTransaction())
			{
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
						ReceivedDate = reliefReceivedViewModel.ReceivedDate,
					};

					newReliefReceived = await _context.Create<ReliefReceived>(newReliefReceived);

					int stockDetailCount = await _context.GetCount<StockDetail>();


					List<StockDetail> stockDetailList = reliefReceivedViewModel.StockDetailViewList.Select((x,index) =>
					{
						stockDetailCount += index;
						string Id = IdGenerator.GenerateId(IdGenerator.PFX_STOCKDETAIL, stockDetailCount);
						return new StockDetail
						{
							Id = Id,
							BatchNumber = newReliefReceived.Id,
							//TypeId = x.TypeId,
							
							ItemId = x.ItemId,
							Quantity = x.Quantity,
							
							FloorId = x.FloorId,
							RackId = x.RackId,
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
				if(reliefReceived is null)
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

		//public async Task<ReliefReceivedViewModel> GetAllInitialSelectionOptions (ReliefReceivedViewModel reliefReceivedViewModel)
		//{
		//	try
		//	{
		//		Task<List<StockType>> StockTypeList = _context.GetAll<StockType>();
		//		Task<List<StockCategory>> StockCategoryList = _context.GetAll<StockCategory>();
		//		Task<List<UoM>> UoMList = _context.GetAll<UoM>();
		//		Task<List<Floor>> FloorList = _context.GetAll<Floor>();

		//		await Task.WhenAll(StockTypeList, StockCategoryList, UoMList, FloorList);

		//		reliefReceivedViewModel.ReliefReceivedFormSelections.StockTypeList = await StockTypeList;
		//		reliefReceivedViewModel.ReliefReceivedFormSelections.StockCategoryList = await StockCategoryList;
		//		reliefReceivedViewModel.ReliefReceivedFormSelections.UoMList = await UoMList;
		//		reliefReceivedViewModel.ReliefReceivedFormSelections.FloorList = await FloorList;

		//		return reliefReceivedViewModel;
		//	}
		//	catch (Exception)
		//	{

		//		throw;
		//	}

		//}

		//public async Task<List<StockItem>> GetAllStockItemBasedOnTypeAndCategory(string stockTypeId, string stockCategoryId)
		//{
		//	try
		//	{
		//		var stockItemList = await _context.StockItem.WhereIsNotArchivedAndDeleted().Where(x=>x.StockTypeId == stockTypeId && x.StockCategoryId == stockCategoryId).ToListAsync();	

		//		return stockItemList;
		//	}
		//	catch (Exception)
		//	{

		//		throw;
		//	}
		//}

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
		
		public async Task<List<StockItem>> GetItemSuggestions(string searchString)
		{
			try
			{

				List<StockItem> suggestions = new();
				suggestions = await  _context.StockItem.Where(x => x.Name.ToLower() == searchString.ToLower()).ToListAsync();
				if (suggestions.Count == 0) { //conditioned to be last option if search results are empty. Expensive Function.
					suggestions = await _context.StockItem.Where(x => PartialTextSearch.CalculateLevenshteinDistance(x.Name, searchString) <= 2).ToListAsync();
				}
				
				return suggestions;
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
