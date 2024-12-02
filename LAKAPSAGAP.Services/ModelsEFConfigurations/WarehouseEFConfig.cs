namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class WarehouseEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<Warehouse>().HasMany(x => x.FloorList).WithOne(x => x.Warehouse).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<Warehouse>().HasMany(x => x.ReliefReceivedList).WithOne(x => x.Warehouse).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.NoAction);
	}
}
