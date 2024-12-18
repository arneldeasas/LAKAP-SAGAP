namespace LAKAPSAGAP.Services.Repositories;

public interface IDashboardRepository
{
	Task<IReadOnlyList<StocksInWarehouse>> GetAllStocksInWarehouses();
	Task<IReadOnlyList<PendingReliefRequest>> GetAllPendingReliefRequests();
	Task<IReadOnlyList<ReceivedDonationsByBarangay>> GetAllReceivedDonationsByBarangays();
	Task<IReadOnlyList<KitStatus>> GetAllKitStatuses();
	Task<int> GetReceivedStocksCount();
	Task<IReadOnlyList<ReleasedOfReliefGoods>> GetAllReleasedOfReliefGoods(int year);
}
