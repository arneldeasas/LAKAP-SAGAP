﻿using Microsoft.AspNetCore.Http;
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
        base.OnModelCreating(builder);

        builder.Entity<UserInfo>().HasOne(x => x.UserAuth).WithMany().HasForeignKey(x => x.UserAuthId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserInfo>().HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserInfo>().HasOne(x => x.AddedBy).WithMany(x => x.AddedUsers).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserInfo>().HasOne(x => x.LastModifiedBy).WithMany(x => x.LasModifiedByList).HasForeignKey(x => x.LastModifiedById).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserInfo>().HasMany(x => x.UserAttachments).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserInfo>().HasMany(x => x.AddedAttachments).WithOne(x => x.AddedBy).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserInfo>().HasMany(x => x.CategoriesCreated).WithOne(x => x.AddedBy).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Attachment>().HasOne(a => a.User).WithMany(x => x.UserAttachments).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Attachment>().HasOne(a => a.AddedBy).WithMany(x => x.AddedAttachments).HasForeignKey(a => a.AddedById).OnDelete(DeleteBehavior.NoAction);

		builder.Entity<Rack>().HasOne(r => r.Floor).WithMany(x => x.Racks).HasForeignKey(r => r.FloorId).OnDelete(DeleteBehavior.NoAction);
		builder.Entity<Rack>().HasMany(r => r.StockDetailList).WithOne(x => x.Rack).HasForeignKey(r => r.RackId).OnDelete(DeleteBehavior.NoAction);

		builder.Entity<Floor>().HasOne(x => x.Warehouse).WithMany(x => x.FloorList).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Floor>().HasMany(x => x.Racks).WithOne(x => x.Floor).HasForeignKey(x => x.FloorId).OnDelete(DeleteBehavior.NoAction);

		builder.Entity<Warehouse>().HasMany(x => x.FloorList).WithOne(x => x.Warehouse).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Warehouse>().HasMany(x => x.ReliefReceivedList).WithOne(x => x.Warehouse).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.NoAction);

        builder.Entity<UoM>().HasMany(x => x.StockItems).WithOne(x => x.UoM).HasForeignKey(x => x.UoMId).OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Category>().HasMany(x => x.StockItems).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Category>().HasOne(x => x.AddedBy).WithMany(x => x.CategoriesCreated).HasForeignKey(x => x.AddedById).OnDelete(DeleteBehavior.Restrict);

		builder.Entity<StockItem>().HasOne(r => r.Category).WithMany(x => x.StockItems).HasForeignKey(r => r.CategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockItem>().HasOne(r => r.UoM).WithMany(x => x.StockItems).HasForeignKey(r => r.UoMId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockItem>().HasMany(r => r.StockDetailList).WithOne(x => x.Item).HasForeignKey(r => r.ItemId).OnDelete(DeleteBehavior.NoAction);

		builder.Entity<StockDetail>().HasOne(r => r.Item).WithMany(x => x.StockDetailList).HasForeignKey(r => r.ItemId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockDetail>().HasOne(r => r.Rack).WithMany(x => x.StockDetailList).HasForeignKey(r => r.RackId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<StockDetail>().HasOne(r => r.BatchDetail).WithMany(x => x.StockDetailList).HasForeignKey(r => r.BatchNumber).OnDelete(DeleteBehavior.NoAction);

        builder.Entity<ReliefReceived>().HasOne(x => x.Warehouse).WithMany(x => x.ReliefReceivedList).HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<ReliefReceived>().HasMany(x => x.StockDetailList).WithOne(x => x.BatchDetail).HasForeignKey(x => x.BatchNumber).OnDelete(DeleteBehavior.NoAction);

		/**
         * Author: Charles Maverick Herrera
         * 
         * PLEASE LETS HAVE 2 MIGRATIONS FIRST
         * 
         * 1. Initialize the tables first (add-migration InitTables). This means that dapat yung seeding ng data is naka comment muna. 
         * 2. Seeding Data (add-migration InitAdminData). This means dapat yung seeding data is uncommented na ha.
         * 
         * AFTER OF 2 MIGRATIONS DONT FORGET TO CHECK IF SAANG DATABASE KAYO NAKATURO.
         * DELETE THAT DATABASE, MAKE SURE TO REMOVE ANY CONNECTIONS ON THAT DB.
         * AFTER THAT BE SURE TO UPDATE DATABASE (update-database)
         */

		SeedData(builder); //Uncomment if mag migrate na ng initial admin data
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
