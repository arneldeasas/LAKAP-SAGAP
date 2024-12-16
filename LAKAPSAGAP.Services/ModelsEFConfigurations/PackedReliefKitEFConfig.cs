namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class PackedReliefKitEFConfig
{
    public static void OnModelCreation(ModelBuilder builder)
    {
        builder.Entity<PackedReliefKit>().HasOne(x => x.Kit).WithMany(x => x.PackedReliefKitList).HasForeignKey(x => x.KitId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<PackedReliefKit>().HasOne(x => x.Rack).WithMany(x => x.PackedReliefKitList).HasForeignKey(x => x.RackId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<PackedReliefKit>().HasOne(x => x.Floor).WithMany(x => x.PackedReliefKitList).HasForeignKey(x => x.FloorId).OnDelete(DeleteBehavior.NoAction);
    }
}
