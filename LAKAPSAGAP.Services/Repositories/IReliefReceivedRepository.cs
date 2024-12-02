using LAKAPSAGAP.Models.ViewModel;

namespace LAKAPSAGAP.Services.Repositories;

public interface IReliefReceivedRepository
{
	public Task<ReliefReceived> CreateReliefReceived(ReliefReceivedViewModel warehouseViewModel);
	public Task<bool> UpdateReliefReceived(ReliefReceivedViewModel warehouseViewModel);
	public Task<ReliefReceived> DeleteReliefReceived(string Id);
	public Task<ReliefReceived> ArchiveReliefReceived(string Id);
	public Task<List<ReliefReceived>> GetAllReliefReceived();
	public Task<ReliefReceivedFormSelections> GetAllInitialSelectionOptions(ReliefReceivedViewModel reliefReceivedViewModel); // will get all initial options/records for the selection inputs, includes stocktype, stockcategory, uom, and floor
	public Task<List<Floor>> GetAllFloorsActive();
	public Task<List<Floor>> GetAllFloorsActiveBasedOnWarehouse(string whseId);
	public Task<List<Rack>> GetAllRacksBasedOnFloor(string floorId);
}
