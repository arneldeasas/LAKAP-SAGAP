namespace LAKAPSAGAP.Services.Repositories;

public interface IRackRepository
{
	Task<string?> CreateRack(RackViewModel rackVM);
	Task<Rack?> GetRack(string rackID);
	Task<List<Rack>> GetAllRack();
	Task<bool> UpdateRack(RackViewModel rackVM);
	//Task DeleteRack(Rack rack); // No Deletion of Data Please
}
