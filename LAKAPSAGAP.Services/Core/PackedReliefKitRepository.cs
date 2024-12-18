
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
            packedReliefKits = await _context.PackedReliefKits
                                        .Include(p => p.Kit)
                                        .Include(p => p.Rack)
                                        .ThenInclude(r => r.Floor)
                                        .Where(x => !x.isArchived)
                                        .ToListAsync();
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
            return content;
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
            var existing = await _context.PackedReliefKits.FirstOrDefaultAsync(x => x.Id == packedReliefKit.Id);
                
            if (existing == null) throw new KeyNotFoundException("Packed Relief Kit not found");
            
            existing.Quantity = packedReliefKit.Quantity;
            existing.FloorId = packedReliefKit.FloorId;
            existing.RackId = packedReliefKit.RackId;
            existing.DatePacked = packedReliefKit.DatePacked;
            existing.PackedBy = packedReliefKit.PackedBy;
            existing.DateUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existing;

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<PackedReliefKit> ArchivePackedReliefKitAsync(PackedReliefKit packedReliefKit)
    {
        try
        {
            var existing = await _context.PackedReliefKits.FirstOrDefaultAsync(x => x.Id == packedReliefKit.Id);

            if (existing == null) throw new KeyNotFoundException("Packed Relief Kit not found");

            existing.isArchived = true;
            existing.DateUpdated = DateTime.UtcNow;

            _context.Entry(existing).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return existing;

        }
        catch (Exception)
        {

            throw;
        }
    }
}
