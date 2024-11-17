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
				Symbol = uoM.Symbol,
				isArchived = uoM.isArchived
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
		try
		{
			List<UoM> uomList = [];
			uomList = await _context.GetAll<UoM>();
			return uomList;
		}
		catch (Exception)
		{
			return [];
		}
	}

	public async Task<UoM?> GetUoM(string uomId)
	{
		try
		{
			UoM? uom = null;
			uom = await _context.GetById<UoM>(uomId);
			return uom;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public async Task<bool> UpdateUoM(UoMViewModel uoM)
	{
		using IDbContextTransaction transaction = _context.Database.BeginTransaction();
		try
		{
			UoM? uom = await _context.GetById<UoM>(uoM.Id);
			if (uom is null) return false;

			uom.Name = uoM.Name;
			uom.Symbol = uoM.Symbol;
			uom.isArchived = uoM.isArchived;

			await _context.UpdateItem(uom);

			await transaction.CommitAsync();
			return true;
		}
		catch (Exception)
		{
			await transaction.RollbackAsync();
			return false;
		}
	}
}
