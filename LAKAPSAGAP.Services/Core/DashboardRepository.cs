
using Mapster;

namespace LAKAPSAGAP.Services.Core;

public class DashboardRepository(
	IWarehouseRepository warehouseRepo
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
}
