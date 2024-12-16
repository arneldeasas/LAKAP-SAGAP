namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class UomEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<UoM>().HasMany(x => x.StockItems).WithOne(x => x.UoM).HasForeignKey(x => x.UoMId).OnDelete(DeleteBehavior.Restrict);
	}
}
