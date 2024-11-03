

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Reflection.Emit;

namespace LAKAPSAGAP.Services
{
    public class MyDbContext : IdentityDbContext<UserAuth>
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<UserAuth> UserAuth { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
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
            .HasOne(r => r.LastModifiedBy)
            .WithMany()
            .HasForeignKey(r => r.LastModifiedById)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<StockType>()
                .HasOne(r => r.LastModifiedBy)
                .WithMany()
                .HasForeignKey(r => r.LastModifiedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<StockCategory>()
                .HasOne(r => r.LastModifiedBy)
                .WithMany()
                .HasForeignKey(r => r.LastModifiedById)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Attachment>()
                .HasOne(a => a.User)
                .WithMany() // Or specify a collection in UserInfo if you have one
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Attachment>()
                .HasOne(a => a.AddedBy)
                .WithMany() // Or specify a collection in UserInfo if you have one
                .HasForeignKey(a => a.AddedById)
                .OnDelete(DeleteBehavior.NoAction); // Set to NoAction or Restrict based on your needs

            builder.Entity<Attachment>()
                .HasOne(a => a.LastModifiedBy)
                .WithMany() // Or specify a collection in UserInfo if you have one
                .HasForeignKey(a => a.LastModifiedById)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateCreated").CurrentValue = DateTime.UtcNow;
                    entry.Property("DateUpdated").CurrentValue = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {


                    entry.Property("DateUpdated").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}
