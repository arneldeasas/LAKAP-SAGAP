using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAKAPSAGAP.Models.ViewModel;

namespace LAKAPSAGAP.Services.Repositories
{
	public interface IReliefReceivedRepository
	{
		public Task<ReliefReceived> CreateReliefReceived(ReliefReceivedViewModel warehouseViewModel);
		public Task<ReliefReceived> UpdateReliefReceived(ReliefReceivedViewModel warehouseViewModel);
		public Task<ReliefReceived> DeleteReliefReceived(string Id);
		public Task<ReliefReceived> ArchiveReliefReceived(string Id);
		public Task<List<ReliefReceived>> GetAllReliefReceived();
		public Task<ReliefReceivedFormSelections> GetAllInitialSelectionOptions(ReliefReceivedViewModel reliefReceivedViewModel); // will get all initial options/records for the selection inputs, includes stocktype, stockcategory, uom, and floor

		public Task<List<Rack>> GetAllRacksBasedOnFloor(string floorId);
	}
}
