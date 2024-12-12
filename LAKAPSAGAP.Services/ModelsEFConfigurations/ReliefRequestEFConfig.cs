

namespace LAKAPSAGAP.Services.ModelsEFConfigurations
{
	public static class ReliefRequestEFConfig
	{
		public static void OnModelCreation(ModelBuilder builder)
		{
			builder.Entity<ReliefRequestDetail>().HasOne(x => x.RequestedBy).WithMany(x => x.ReliefRequestDetailsList).HasForeignKey(x=>x.RequestedById).OnDelete(DeleteBehavior.NoAction);
			builder.Entity<ReliefRequestDetail>().HasMany(x => x.AttachmentList).WithOne(x => x.ReliefRequest).HasForeignKey(x => x.ReliefRequestId).OnDelete(DeleteBehavior.NoAction);
			builder.Entity<ReliefRequestDetail>().HasMany(x =>x.RequestList).WithOne(x=>x.ReliefRequest).HasForeignKey(x=>x.ReliefRequestId).OnDelete(DeleteBehavior.NoAction);
		}
	}
}
