
namespace LAKAPSAGAP.Services
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<UserInfo> UserInfo { get; set; }
        // Add other DbSets for your models here
    }
}
