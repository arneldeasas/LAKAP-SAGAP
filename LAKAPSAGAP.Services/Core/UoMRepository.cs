using LAKAPSAGAP.Services.Core.Helpers;
using Microsoft.EntityFrameworkCore.Storage;

namespace LAKAPSAGAP.Services.Core;

public class UoMRepository : IUoMRepository
{
    MyDbContext _context { get; set; }

    public UoMRepository(MyDbContext context) => _context = context;

    public async Task<string?> CreateUoM(UoMViewModel uoM)
    {
        using IDbContextTransaction transaction = _context.Database.BeginTransaction();
        string? newUoMId = null;

        try
        {
            int uomCount = await _context.GetCount<UoM>();
            string uomId = IdGenerator.GenerateId(IdGenerator.PFX_UOM, uomCount);

            UoM newUom = new()
            {
                Id = uomId,
                Name = uoM.Name,
                Symbol = uoM.Symbol
            };

            await _context.UoMs.AddAsync(newUom);
            await transaction.CommitAsync();
            newUoMId = uomId;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }

        return newUoMId;
    }

    public Task<List<UoM>> GetAllUoM()
    {
        throw new NotImplementedException();
    }

    public Task<UoM> GetUoM(string uomId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUoM(UoMViewModel uoM)
    {
        throw new NotImplementedException();
    }
}
