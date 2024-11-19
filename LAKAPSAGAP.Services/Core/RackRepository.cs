using LAKAPSAGAP.Services.Core.Helpers;
using Microsoft.EntityFrameworkCore.Storage;

namespace LAKAPSAGAP.Services.Core;

public class RackRepository(MyDbContext context) : IRackRepository
{
	readonly MyDbContext _context = context;

	public async Task<string?> CreateRack(RackViewModel rackVM)
	{
		using IDbContextTransaction transaction = _context.Database.BeginTransaction();
		string? newRackId = null;

		try
		{
			int rackCount = await _context.GetCount<Rack>();
			string rackId = IdGenerator.GenerateId(IdGenerator.PFX_RACK, rackCount);

			await _context.Create<Rack>(new()
			{
				Id = rackId,
				Name = rackVM.Name,
				isArchived = rackVM.isArchived
			});

			await transaction.CommitAsync();
			newRackId = rackId;
		}
		catch (Exception)
		{
			await transaction.RollbackAsync();
		}

		return newRackId;
	}

	public async Task<List<Rack>> GetAllRack()
	{
		try
		{
			List<Rack> rackList = [];
			rackList = await _context.GetAllNotDeleted<Rack>();
			return rackList;
		}
		catch (Exception)
		{
			return [];
		}
	}

	public async Task<Rack?> GetRack(string rackID)
	{
		try
		{
			Rack? rack = null;
			rack = await _context.GetByIdIncludeArchivedsOnly<Rack>(rackID);
			return rack;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public async Task<bool> UpdateRack(RackViewModel rackVM)
	{
		using IDbContextTransaction transaction = _context.Database.BeginTransaction();
		try
		{
			Rack? rack = await _context.GetByIdIncludeArchivedsOnly<Rack>(rackVM.Id);
			if (rack is null) return false;

			rack.Name = rackVM.Name;
			rack.FloorId = rackVM.FloorId;
			rack.isArchived = rackVM.isArchived;

			await _context.UpdateItem(rack);

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
