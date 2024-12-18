namespace LAKAPSAGAP.Services.Repositories;

public interface IDashboardRepository
{
	Task<IReadOnlyList<StocksInWarehouse>> GetAllStocksInWarehouses();
}
