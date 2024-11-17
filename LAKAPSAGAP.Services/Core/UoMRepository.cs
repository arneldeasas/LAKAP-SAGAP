using LAKAPSAGAP.Services.Core.Helpers;
using Microsoft.EntityFrameworkCore.Storage;

namespace LAKAPSAGAP.Services.Core;

public class UoMRepository : IUoMRepository
{
    readonly MyDbContext _context;

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

            await _context.Create<UoM>(newUom);
            await transaction.CommitAsync();
            newUoMId = uomId;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }

        return newUoMId;
    }

    public async Task<List<UoM>> GetAllUoM()
    {
		using IDbContextTransaction transaction = _context.Database.BeginTransaction();
        List<UoM> uomList = [];

		try
        {
            uomList = await _context.GetAll<UoM>();
        }
        catch (Exception)
        {
			await transaction.RollbackAsync();
        }

        return uomList;
    }

    public async Task<UoM?> GetUoM(string uomId)
    {
		using IDbContextTransaction transaction = _context.Database.BeginTransaction();
        UoM? uom = null;

		try
		{
			uom = await _context.GetById<UoM>(uomId);
		}
		catch (Exception)
		{
			await transaction.RollbackAsync();
		}

		return uom;
	}

    public Task UpdateUoM(UoMViewModel uoM)
    {
        throw new NotImplementedException();
    }
}
