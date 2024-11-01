

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.Services
{
    public class MyDbContext : IdentityDbContext<UserAuth>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<UserAuth> UserAuth {  get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserInfo.Attachment> Attachment { get; set; }
        public DbSet<ReliefReceived> ReliefReceiveds { get; set; }
        public DbSet<ReliefReceived.StockDetails> StockDetails { get; set; }
        public DbSet<ReliefReceived.StockDetails.StockType> StockType { get; set; }
        public DbSet<ReliefReceived.StockDetails.StockItem> StockItems { get; set; }
        public DbSet<ReliefReceived.StockDetails.StockCategory> StockCategories { get; set; }
        // Add other DbSets for your models here
    }
}
