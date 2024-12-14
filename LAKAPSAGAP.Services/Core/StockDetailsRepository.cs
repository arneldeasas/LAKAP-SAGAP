
namespace LAKAPSAGAP.Services.Core;

public class StockDetailsRepository : IStockDetailsRepository
{

    readonly MyDbContext _context;
    public StockDetailsRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<StockDetail>> GetAllStockDetailsActive()
    {
        try
        {
            var stockDetailList = _context.StockDetails.AsNoTracking()
                                                       .Include(x => x.Item)
                                                       .Include(x => x.Item.UoM)
                                                       .Include(x => x.Item.Category)
                                                       .ToList();
            
            return stockDetailList;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
