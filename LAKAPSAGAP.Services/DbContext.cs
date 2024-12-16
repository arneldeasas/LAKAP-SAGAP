using LAKAPSAGAP.Services.ModelsEFConfigurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace LAKAPSAGAP.Services;

public class MyDbContext(
	DbContextOptions<MyDbContext> options,
	HttpContextAccessor contextAccessor) : IdentityDbContext<UserAuth>(options)
{
	HttpContextAccessor _contextAccessor { get; set; } = contextAccessor;

	public DbSet<UserAuth> UserAuths { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<Attachment> Attachments { get; set; }

    public DbSet<Rack> Racks { get; set; }
    public DbSet<Floor> Floors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<UoM> UoMs { get; set; }
    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<StockDetail> StockDetails { get; set; }
    public DbSet<ReliefReceived> ReliefReceiveds { get; set; }

    public DbSet<Kit> Kits { get; set; }
	public DbSet<KitComponent> KitComponents { get; set; }
	public DbSet<PackedReliefKit> PackedReliefKits { get; set; }
	public DbSet<ReliefRequestDetail> ReliefRequests { get; set; }
	public DbSet<Request> RequestItems { get; set; }
	public DbSet<RequestAttachment> RequestAttachments { get; set; }
	public DbSet<ReliefSent> ReliefSents { get; set; }

    public DbSet<ReliefSentItem> ReliefSentItems { get; set; }
	public DbSet<ReliefSentKit> ReliefSentKits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var loggerFactory = LoggerFactory.Create(builder =>
		{
			builder.AddFilter("Microsoft.AspNetCore.Hosting", LogLevel.None);
			builder.AddFilter("Microsoft.AspNetCore.Routing", LogLevel.None);
			builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);
			builder.AddFilter("Microsoft.AspNetCore.DataProtection", LogLevel.None);
		});

		optionsBuilder.UseLoggerFactory(loggerFactory);
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder builder)
    {
		UserInfoEFConfig.OnModelCreation(builder);
		AttachmentEFConfig.OnModelCreation(builder);
		RackEFConfig.OnModelCreation(builder);
		FloorEFConfig.OnModelCreation(builder);
		WarehouseEFConfig.OnModelCreation(builder);
		UomEFConfig.OnModelCreation(builder);
		CategoryEFConfig.OnModelCreation(builder);
		StockItemEFConfig.OnModelCreation(builder);
		StockDetailsEFConfig.OnModelCreation(builder);
		ReliefReceivingEFConfig.OnModelCreation(builder);
		KitEFConfig.OnModelCreation(builder);
		ReliefRequestEFConfig.OnModelCreation(builder);
		/**
         * Author: Charles Maverick Herrera
         * 
         * 1. Delete any existing lakap-sagap db if you got this new update.
         * 2. Remove the Migrations folder (make sure no migration left)
         * 3. Add one migration (prefer: add-migration InitializeDB)
         * 4. Start the app.
         * 
         * Note: There's no need to rebuild the app or remigrate if you already have the db with same migration history.
         */

		SeedData(builder);

        base.OnModelCreating(builder);
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

                string? userId = UserInfos.FirstOrDefault(x => x.UserAuthId == actionUserAuthId)?.Id;

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

	static void SeedData(ModelBuilder builder)
	{
		string CSWD_ADMIN_ROLE_ID = Guid.NewGuid().ToString();
		string CSWD_HEAD_ROLE_ID = Guid.NewGuid().ToString();
		string BARANGAY_REP_ROLE_ID = Guid.NewGuid().ToString();
		string CSWD_ADMIN_USER_AUTH_ID = Guid.NewGuid().ToString();
		string CSWD_HEAD_USER_AUTH_ID = Guid.NewGuid().ToString();
		string BARANGAY_REP_USER_AUTH_ID = Guid.NewGuid().ToString();

		builder.Entity<IdentityRole>().HasData(
			new IdentityRole() { Id = CSWD_ADMIN_ROLE_ID, Name = "CSWD Administration Staff", NormalizedName = "CSWD_ADMINISTRATION_STAFF" },
			new IdentityRole() { Id = CSWD_HEAD_ROLE_ID, Name = "CSWD Office Head", NormalizedName = "CSWD_OFFICE_HEAD" },
			new IdentityRole() { Id = BARANGAY_REP_ROLE_ID, Name = "Barangay Representative", NormalizedName = "BARANGAY_REPRESENTATIVE" }
		);

		builder.Entity<UserAuth>().HasData(new UserAuth()
		{
			Id = CSWD_ADMIN_USER_AUTH_ID,
			UserName = "admin",
			Email = "admin@gmail.com",
			NormalizedUserName = "ADMIN",
			NormalizedEmail = "ADMIN@GMAIL.COM",
			EmailConfirmed = true,
			PasswordHash = "AQAAAAIAAYagAAAAENkx43Zrg2/tiKZoCx7TL3mMQTIN8rXAZ3Z5WOldLFjUCL9lcdeAlUlZkA8Pyq9wRg==" // admin1234
		});

		builder.Entity<Attachment>().HasData(
			new Attachment()
			{
				Id = "ATT_001",
				Url = "wwwroot\\attachments\\default_user_image.png",
				UserId = "ACC_001",
				AddedById = "ACC_001",
				DateCreated = DateTime.Now,
				DateUpdated = DateTime.Now,
			}
		);

		builder.Entity<UserInfo>().HasData(
			new UserInfo()
			{
				Id = "ACC_001",
				FirstName = "LakapSagap",
				MiddleName = "Capstone",
				LastName = "Admin",
				Barangay = "Karuhatan",
				Email = "admin@gmail.com",
				Phone = "09123456789",
				AddedById = "ACC_001",
				UserAuthId = CSWD_ADMIN_USER_AUTH_ID,
				RoleId = CSWD_ADMIN_ROLE_ID,
				DateCreated = DateTime.Now,
				DateUpdated = DateTime.Now
			}
		);
	}

}
