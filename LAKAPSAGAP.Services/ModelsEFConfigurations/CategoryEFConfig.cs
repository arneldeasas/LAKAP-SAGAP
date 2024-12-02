namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class CategoryEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<Category>().HasMany(x => x.StockItems).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<Category>().HasOne(x => x.AddedBy).WithMany(x => x.CategoriesCreated).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);
	}
}
