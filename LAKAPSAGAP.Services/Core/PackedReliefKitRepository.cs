
using LAKAPSAGAP.Models.Models;
using LAKAPSAGAP.Models.ViewModels;
using LAKAPSAGAP.Services.Core.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore.Storage;

namespace LAKAPSAGAP.Services.Core;

public class PackedReliefKitRepository(MyDbContext context) : IPackedReliefKitRepository
{
    readonly MyDbContext _context = context;

    public async Task<PackedReliefKit> CreatePackedReliefKitAsync(PackedReliefKit packedReliefKit)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            int count = await _context.GetCount<PackedReliefKit>();
            string Id = IdGenerator.GenerateId(IdGenerator.PFX_PACKEDKIT, count);
            packedReliefKit.Id = Id;

            PackedReliefKit newpackedReliefKit = null;
            newpackedReliefKit = await _context.Create(packedReliefKit);

            await transaction.CommitAsync();
            return newpackedReliefKit;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<List<PackedReliefKit>> GetAllPackedReliefKitAsync()
    {
        try
        {
            List<PackedReliefKit> packedReliefKits = [];
            packedReliefKits = await _context.GetAll<PackedReliefKit>();
            return packedReliefKits;
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task<PackedReliefKit> GetPackedReliefKitAsync(string id)
    {
        try
        {
            PackedReliefKit content = await _context.GetById<PackedReliefKit>(id);
            if (content is null) throw new Exception("No Content found with that ID");
            return content ;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PackedReliefKit> UpdatePackedReliefKitAsync(PackedReliefKit packedReliefKit)
    {
        try
        {
            return await _context.UpdateItem<PackedReliefKit>(packedReliefKit);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
