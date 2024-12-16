namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class FloorEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<Floor>().HasOne(x => x.Warehouse).WithMany(x => x.FloorList).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<Floor>().HasMany(x => x.Racks).WithOne(x => x.Floor).HasForeignKey(x => x.FloorId).OnDelete(DeleteBehavior.NoAction);
	}
}
