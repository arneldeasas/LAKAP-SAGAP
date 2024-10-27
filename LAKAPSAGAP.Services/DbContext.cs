
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LAKAPSAGAP.Services
{
    public class MyDbContext : IdentityDbContext<UserAuth>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserAuth> UserAuth {  get; set; }
        // Add other DbSets for your models here
    }
}
