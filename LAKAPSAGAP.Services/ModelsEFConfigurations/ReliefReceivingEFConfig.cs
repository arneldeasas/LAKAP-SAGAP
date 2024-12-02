namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class ReliefReceivingEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<ReliefReceived>().HasOne(x => x.Warehouse).WithMany(x => x.ReliefReceivedList).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<ReliefReceived>().HasMany(x => x.StockDetailList).WithOne(x => x.BatchDetail).HasForeignKey(x => x.BatchNo).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<ReliefReceived>().Property(o => o.AcquisitionType).HasConversion<string>();
	}
}
