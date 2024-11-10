﻿// <auto-generated />
using System;
using LAKAPSAGAP.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LAKAPSAGAP.Services.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Attachment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserInfoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("UserId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Floor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Floor");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Rack", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("FloorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FloorId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("FloorId");

                    b.HasIndex("FloorId1");

                    b.HasIndex("LastModifiedById");

                    b.ToTable("Rack");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.ReliefReceived", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReceivedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReceivedFrom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReliefType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TruckPlateNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ReliefReceived");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.StockCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.ToTable("StockCategory");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.StockDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BatchNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FloorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("RackId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UoMId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("BatchNumber");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FloorId");

                    b.HasIndex("ItemId");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("RackId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UoMId");

                    b.ToTable("StockDetail");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.StockItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockCategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StockTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("StockCategoryId");

                    b.HasIndex("StockTypeId");

                    b.ToTable("StockItem");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.StockType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.ToTable("StockType");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.UoM", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.ToTable("UoM");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.UserAuth", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.UserInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Barangay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserAuthId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("RoleId");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Warehouse", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("LastModifiedById");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Attachment", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", null)
                        .WithMany("Attachments")
                        .HasForeignKey("UserInfoId");

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Floor", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.Warehouse", null)
                        .WithMany("FloorList")
                        .HasForeignKey("WarehouseId");

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Rack", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.Floor", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.Floor", null)
                        .WithMany("RackList")
                        .HasForeignKey("FloorId1");

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.Navigation("AddedBy");

                    b.Navigation("Floor");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.ReliefReceived", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.Warehouse", "Warehouse")
                        .WithMany("ReliefReceivedList")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.StockCategory", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.StockDetail", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.ReliefReceived", "BatchDetail")
                        .WithMany("StockDetailList")
                        .HasForeignKey("BatchNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.StockCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.Floor", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.StockItem", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.Rack", "Rack")
                        .WithMany()
                        .HasForeignKey("RackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.StockType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.UoM", "UoM")
                        .WithMany()
                        .HasForeignKey("UoMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddedBy");

                    b.Navigation("BatchDetail");

                    b.Navigation("Category");

                    b.Navigation("Floor");

                    b.Navigation("Item");

                    b.Navigation("LastModifiedBy");

                    b.Navigation("Rack");

                    b.Navigation("Type");

                    b.Navigation("UoM");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.StockItem", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LAKAPSAGAP.Models.Models.StockCategory", "StockCategory")
                        .WithMany()
                        .HasForeignKey("StockCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.StockType", "StockType")
                        .WithMany()
                        .HasForeignKey("StockTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");

                    b.Navigation("StockCategory");

                    b.Navigation("StockType");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.StockType", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.UoM", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.UserInfo", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Warehouse", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");

                    b.HasOne("LAKAPSAGAP.Models.Models.UserInfo", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.Navigation("AddedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAKAPSAGAP.Models.Models.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LAKAPSAGAP.Models.Models.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Floor", b =>
                {
                    b.Navigation("RackList");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.ReliefReceived", b =>
                {
                    b.Navigation("StockDetailList");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.UserInfo", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("LAKAPSAGAP.Models.Models.Warehouse", b =>
                {
                    b.Navigation("FloorList");

                    b.Navigation("ReliefReceivedList");
                });
#pragma warning restore 612, 618
        }
    }
}
