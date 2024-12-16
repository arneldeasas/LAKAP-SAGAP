namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class UserInfoEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<UserInfo>().HasOne(x => x.UserAuth).WithMany().HasForeignKey(x => x.UserAuthId).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<UserInfo>().HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<UserInfo>().HasOne(x => x.AddedBy).WithMany(x => x.AddedUsers).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<UserInfo>().HasOne(x => x.LastModifiedBy).WithMany(x => x.LasModifiedByList).HasForeignKey(x => x.LastModifiedById).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<UserInfo>().HasMany(x => x.UserAttachments).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<UserInfo>().HasMany(x => x.AddedAttachments).WithOne(x => x.AddedBy).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<UserInfo>().HasMany(x => x.CategoriesCreated).WithOne(x => x.AddedBy).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<UserInfo>().HasMany(x => x.ReliefRequestDetailList).WithOne(x => x.AddedBy).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);
		builder.Entity<UserInfo>().HasMany(x => x.RequestAttachmentList).WithOne(x => x.AddedBy).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);
	}
}
