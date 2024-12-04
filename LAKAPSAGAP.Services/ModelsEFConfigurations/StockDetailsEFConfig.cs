namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class StockDetailsEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<StockDetail>().HasOne(r => r.Item).WithMany(x => x.StockDetailList).HasForeignKey(r => r.ItemId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<StockDetail>().HasOne(r => r.Rack).WithMany(x => x.StockDetailList).HasForeignKey(r => r.RackId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<StockDetail>().HasOne(r => r.BatchDetail).WithMany(x => x.StockDetailList).HasForeignKey(r => r.BatchNo).OnDelete(DeleteBehavior.NoAction);
	}
}
