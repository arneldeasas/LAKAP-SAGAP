namespace LAKAPSAGAP.Services.ModelsEFConfigurations;

public static class AttachmentEFConfig
{
	public static void OnModelCreation(ModelBuilder builder)
	{
		builder.Entity<Attachment>().HasOne(a => a.User).WithMany(x => x.UserAttachments).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<Attachment>().HasOne(a => a.AddedBy).WithMany(x => x.AddedAttachments).HasForeignKey(a => a.AddedById).OnDelete(DeleteBehavior.NoAction);
	}
}
