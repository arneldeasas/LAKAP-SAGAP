
using Mapster;

namespace LAKAPSAGAP.Services.Core;

public class DashboardRepository(
	IWarehouseRepository warehouseRepo,
	IReliefRequestRepository reliefReqRepo,
	IKittingRepository kitRepo,
	IReliefReceivedRepository receivingRepo
	) : IDashboardRepository
{
	public async Task<IReadOnlyList<StocksInWarehouse>> GetAllStocksInWarehouses()
	{
		IReadOnlyList<StocksInWarehouse> stocksInWarehouses = [];
		try
		{
			var res = (await warehouseRepo.GetAllWarehouses())
				.Select(warehouse => new StocksInWarehouse()
					{
						WarehouseId = warehouse.Id,
						WarehouseName = warehouse.Name,
						Total = warehouse.FloorList.Sum(floor => floor.Racks.Sum(rack => rack.StockDetailList.Sum(sd => sd.Quantity)))
					}).ToList();
			if (res is not null && res.Count != 0) stocksInWarehouses = res.Adapt<IReadOnlyList<StocksInWarehouse>>();
			return stocksInWarehouses;
		}
		catch (Exception)
		{
			return stocksInWarehouses;
		}
	}

	public async Task<IReadOnlyList<PendingReliefRequest>> GetAllPendingReliefRequests()
	{
		IReadOnlyList<PendingReliefRequest> pendingReliefRequests = [];
		try
		{
			var res = (await reliefReqRepo.GetAllRequestsAsync())
				.Where(x => x.Status == RequestStatus.pending)
				.Select(pendingReliefRequest => new PendingReliefRequest()
				{
					RequestDate = pendingReliefRequest.DateCreated,
					MainReason = pendingReliefRequest.SpecificReason,
					RequestorName = $"{pendingReliefRequest.RequestedBy.FirstName} {pendingReliefRequest.RequestedBy.LastName}",
					RequestorEmail = pendingReliefRequest.RequestedBy.Email,
					Barangay = pendingReliefRequest.Organization,
					Image = pendingReliefRequest.RequestedBy.AddedAttachments.First().Url ?? "wwwroot\\attachments\\default_user_image.png",
					KitName = ""
				}).ToList();
			if (res is not null && res.Count > 0) pendingReliefRequests = res.Adapt<IReadOnlyList<PendingReliefRequest>>();
			return pendingReliefRequests;
		}
		catch (Exception)
		{
			return pendingReliefRequests;
		}
	}

	public async Task<IReadOnlyList<ReceivedDonationsByBarangay>> GetAllReceivedDonationsByBarangays()
	{
		IReadOnlyList<ReceivedDonationsByBarangay> rcvdDonationsByBarangays = [];
		try
		{
			var res = (await reliefReqRepo.GetAllRequestsAsync())
				.Where(x => x.Status == RequestStatus.delivered)
				.GroupBy(x => x.Organization)
				.Select(group => new ReceivedDonationsByBarangay()
				{
					BarangayName = group.Key,
					Total = group.Sum(req => req.RequestList.Sum(r => r.Quantity))
				}).ToList();
			if (res is not null && res.Count > 0) rcvdDonationsByBarangays = res.Adapt<IReadOnlyList<ReceivedDonationsByBarangay>>();
			return rcvdDonationsByBarangays;
		}
		catch (Exception)
		{
			return rcvdDonationsByBarangays;
		}
	}

	public async Task<IReadOnlyList<KitStatus>> GetAllKitStatuses()
	{
		IReadOnlyList<KitStatus> kitStatuses = [];
		try
		{
			var res = (await kitRepo.GetAllKit())
				.Select(kit => new KitStatus()
				{
					KitName = kit.Name,
					Total = kit.PackedReliefKitList.Sum(pk => pk.Quantity)
				}).ToList();
			if (res is not null && res.Count > 0) kitStatuses = res.Adapt<IReadOnlyList<KitStatus>>();
			return kitStatuses;
		}
		catch (Exception)
		{
			return kitStatuses;
		}
	}

	public async Task<int> GetReceivedStocksCount()
	{
		try
		{
			int total = 0;
			List<ReliefReceived> batches = await receivingRepo.GetAllBatches();
			total = batches.Sum(b => b.StockDetailList.Sum(sd => sd.Quantity));
			return total;
		}
		catch (Exception)
		{
			return 0;
		}
	}

	public async Task<IReadOnlyList<ReleasedOfReliefGoods>> GetAllReleasedOfReliefGoods(int year)
	{
		IReadOnlyList<ReleasedOfReliefGoods> releasedOfReliefGoods = [];
		try
		{
			var res = (await reliefReqRepo.GetAllRequestsAsync())
				.Where(x => x.Status == RequestStatus.delivered && x.DateUpdated.Year == year)
				.Select(rrd => new ReleasedOfReliefGoods()
				{
					ReleasedDate = DateOnly.FromDateTime(rrd.DateUpdated),
					Total = rrd.RequestList.Sum(reqLst => reqLst.Quantity)
				}).ToList();
			if (res is not null && res.Count > 0) releasedOfReliefGoods = res.Adapt<IReadOnlyList<ReleasedOfReliefGoods>>();
			return releasedOfReliefGoods;
		}
		catch (Exception)
		{
			return releasedOfReliefGoods;
		}
	}
}
