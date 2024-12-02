namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class RackEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<Rack>().HasOne(r => r.Floor).WithMany(x => x.Racks).HasForeignKey(r => r.FloorId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<Rack>().HasMany(r => r.StockDetailList).WithOne(x => x.Rack).HasForeignKey(r => r.RackId).OnDelete(DeleteBehavior.NoAction);
	}
}
