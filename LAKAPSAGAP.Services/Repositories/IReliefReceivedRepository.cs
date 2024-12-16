using LAKAPSAGAP.Models.ViewModel;

namespace LAKAPSAGAP.Services.Repositories;

public interface IReliefReceivedRepository
{
	Task<ReliefReceived> CreateReliefReceived(ReliefReceived warehouseViewModel);
	Task<bool> UpdateReliefReceived(ReliefReceived warehouseViewModel);
	Task<ReliefReceived> DeleteReliefReceived(string Id);
	Task<ReliefReceived> ArchiveReliefReceived(string Id);
	Task<List<ReliefReceived>> GetAllReliefReceived();
	Task<ReliefReceivedFormSelections> GetAllInitialSelectionOptions(ReliefReceivedViewModel reliefReceivedViewModel); // will get all initial options/records for the selection inputs, includes stocktype, stockcategory, uom, and floor
	Task<List<Floor>> GetAllFloorsActive();
	Task<List<Floor>> GetAllFloorsActiveBasedOnWarehouse(string whseId);
	Task<List<Rack>> GetAllRacksBasedOnFloor(string floorId);
	Task<List<ReliefReceived>> GetAllBatches();
}
