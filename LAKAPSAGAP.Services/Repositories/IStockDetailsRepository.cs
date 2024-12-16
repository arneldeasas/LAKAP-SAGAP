namespace LAKAPSAGAP.Services.Repositories;

public interface IStockDetailsRepository
{
    Task<List<StockDetail>> GetAllStockDetailsActive();
}
