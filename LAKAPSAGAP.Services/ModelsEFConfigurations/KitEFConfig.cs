namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class KitEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<Kit>().HasMany(x => x.KitComponentList).WithOne(x => x.Kit).OnDelete(DeleteBehavior.NoAction);
	}
}
