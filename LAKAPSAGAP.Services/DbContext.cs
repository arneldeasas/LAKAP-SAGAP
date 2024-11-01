

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LAKAPSAGAP.Services
{
    public class MyDbContext : IdentityDbContext<UserAuth>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<UserAuth> UserAuth { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserInfo.Attachment> Attachment { get; set; }
        public DbSet<ReliefReceived> ReliefReceived { get; set; }
        public DbSet<StockDetail> StockDetail { get; set; }
        public DbSet<StockType> StockType { get; set; }
        public DbSet<StockItem> StockItem { get; set; }
        public DbSet<StockCategory> StockCategory { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        // Add other DbSets for your models here

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<StockItem>()
            .HasOne(si => si.AddedBy)
            .WithMany()
            .HasForeignKey(si => si.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<StockType>()
                .HasOne(st => st.AddedBy)
                .WithMany()
                .HasForeignKey(st => st.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<StockCategory>()
                .HasOne(sc => sc.AddedBy)
                .WithMany()
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
