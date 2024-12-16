namespace LAKAPSAGAP.Services.Repositories;

public interface IMaintenanceRepository
{
	public Task<StockItem> CreateStockItem(StockItemViewModel stockItemViewModel);
	public Task<StockItem> UpdateStockItem(StockItemViewModel stockItemViewModel);
	public Task<StockItem> DeleteStockItem(string Id);
	public Task<StockItem> ArchiveStockItem(string Id);
	public Task<StockItem> GetStockItemById(string Id);
	public Task<List<StockItem>> GetAllStockItems();

	public Task<UoM> CreateUoM(UoMViewModel uomViewModel);
	public Task<UoM> UpdateUoM(UoMViewModel uoMViewModel);
	public Task<UoM> DeleteUoM(string Id);
	public Task<UoM> ArchiveUoM(string Id);
	public Task<UoM> GetUoMById(string Id);
	public Task<List<UoM>> GetAllUoMs();

	public Task<Floor> CreateFloor(FloorViewModel floorViewModel);
	public Task<Floor> UpdateFloor(FloorViewModel floorViewModel);
	public Task<Floor> DeleteFloor(string Id);
	public Task<Floor> ArchiveFloor(string Id);
	public Task<Floor> GetFloorById(string Id);
	public Task<List<Floor>> GetAllFloors();

	public Task<Rack> CreateRack(RackViewModel rackViewModel);
	public Task<Rack> UpdateRack(RackViewModel rackViewModel);
	public Task<Rack> DeleteRack(string Id);
	public Task<Rack> ArchiveRack(string Id);
	public Task<Rack> GetRackById(string Id);
	public Task<List<Rack>> GetAllRacks();
}
