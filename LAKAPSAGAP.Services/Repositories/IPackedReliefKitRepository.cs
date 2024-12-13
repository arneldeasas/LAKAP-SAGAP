namespace LAKAPSAGAP.Services.Repositories;

public interface IPackedReliefKitRepository
{
    Task<List<PackedReliefKit>> GetAllPackedReliefKitAsync();
    Task<PackedReliefKit> GetPackedReliefKitAsync(string id);
    Task<PackedReliefKit> CreatePackedReliefKitAsync(PackedReliefKit packedReliefKit);
    Task<PackedReliefKit> UpdatePackedReliefKitAsync(PackedReliefKit packedReliefKit);
    Task<PackedReliefKit> ArchivePackedReliefKitAsync(PackedReliefKit packedReliefKit);
}
