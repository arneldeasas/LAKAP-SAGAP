using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace LAKAPSAGAP.Services;

public class MyDbContext : IdentityDbContext<UserAuth>
{
    HttpContextAccessor _contextAccessor { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options, HttpContextAccessor contextAccessor) : base(options)
    {
        _contextAccessor = contextAccessor;
    }

    public DbSet<UserAuth> UserAuth { get; set; }
    public DbSet<UserInfo> UserInfo { get; set; }
    public DbSet<IdentityRole> Role { get; set; }
    public DbSet<Attachment> Attachment { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<ReliefReceived> ReliefReceived { get; set; }
    public DbSet<StockDetail> StockDetail { get; set; }
    public DbSet<StockItem> StockItem { get; set; }
    public DbSet<StockCategory> StockCategory { get; set; }
    public DbSet<UoM> UoMs { get; set; }
    public DbSet<Floor> Floor { get; set; }
    public DbSet<Rack> Rack { get; set; }


    // Add other DbSets for your models here

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserInfo>().HasOne(r => r.AddedBy).WithMany().HasForeignKey(r => r.AddedById).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<UserInfo>().HasOne(r => r.LastModifiedBy).WithMany().HasForeignKey(r => r.LastModifiedById).OnDelete(DeleteBehavior.NoAction);

        builder.Entity<StockCategory>().HasOne(r => r.LastModifiedBy).WithMany().HasForeignKey(r => r.LastModifiedById).OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Attachment>().HasOne(a => a.User)
            .WithMany() // Or specify a collection in UserInfo if you have one
            .HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Attachment>().HasOne(a => a.AddedBy)
            .WithMany() // Or specify a collection in UserInfo if you have one
            .HasForeignKey(a => a.AddedById).OnDelete(DeleteBehavior.NoAction); // Set to NoAction or Restrict based on your needs
        builder.Entity<Attachment>().HasOne(a => a.LastModifiedBy)
            .WithMany() // Or specify a collection in UserInfo if you have one
            .HasForeignKey(a => a.LastModifiedById).OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Rack>().HasOne(r => r.Floor).WithMany().HasForeignKey(r => r.FloorId).OnDelete(DeleteBehavior.NoAction);
        
        builder.Entity<StockItem>().HasOne(r => r.StockCategory).WithMany().HasForeignKey(r => r.StockCategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockItem>().HasOne(r => r.UoM).WithMany().HasForeignKey(r => r.UoMId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockItem>().HasOne(r => r.LastModifiedBy).WithMany().HasForeignKey(r => r.LastModifiedById).OnDelete(DeleteBehavior.NoAction);
        
        builder.Entity<StockDetail>().HasOne(r => r.Item).WithMany().HasForeignKey(r => r.ItemId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockDetail>().HasOne(r => r.Category).WithMany().HasForeignKey(r => r.CategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockDetail>().HasOne(r => r.UoM).WithMany().HasForeignKey(r => r.UoMId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockDetail>().HasOne(r => r.Rack).WithMany().HasForeignKey(r => r.RackId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockDetail>().HasOne(r => r.BatchDetail).WithMany().HasForeignKey(r => r.BatchNumber).OnDelete(DeleteBehavior.NoAction);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            bool hasDateCreated = entry.Properties.Any(p => p.Metadata.Name == "DateCreated");
            bool hasDateUpdated = entry.Properties.Any(p => p.Metadata.Name == "DateUpdated");
            bool hasAddedById = entry.Properties.Any(p => p.Metadata.Name == "AddedById");
            bool hasModifiedById = entry.Properties.Any(p => p.Metadata.Name == "LastModifiedById");
            if (hasDateCreated && hasDateUpdated && hasAddedById && hasModifiedById)
            {
                string? actionUserAuthId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                string? userId = UserInfo.FirstOrDefault(x => x.UserAuthId == actionUserAuthId)?.Id;

                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateCreated").CurrentValue = DateTime.UtcNow;
                    entry.Property("DateUpdated").CurrentValue = DateTime.UtcNow;
                    entry.Property("AddedById").CurrentValue = userId;
                    entry.Property("LastModifiedById").CurrentValue = userId;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("LastModifiedById").CurrentValue = userId;
                    entry.Property("DateUpdated").CurrentValue = DateTime.UtcNow;
                }
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
