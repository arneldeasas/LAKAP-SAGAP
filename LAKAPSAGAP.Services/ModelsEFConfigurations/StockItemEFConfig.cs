namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class StockItemEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<StockItem>().HasOne(r => r.Category).WithMany(x => x.StockItems).HasForeignKey(r => r.CategoryId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<StockItem>().HasOne(r => r.UoM).WithMany(x => x.StockItems).HasForeignKey(r => r.UoMId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<StockItem>().HasMany(r => r.StockDetailList).WithOne(x => x.Item).HasForeignKey(r => r.ItemId).OnDelete(DeleteBehavior.NoAction);
	}
}
