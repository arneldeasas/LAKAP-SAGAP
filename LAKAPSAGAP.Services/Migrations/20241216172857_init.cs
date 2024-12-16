using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LAKAPSAGAP.Services.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAuthId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfos_AspNetUsers_UserAuthId",
                        column: x => x.UserAuthId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfos_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfos_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachments_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachments_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReliefSents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReliefRequestId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliefSents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReliefSents_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefSents_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UoMs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UoMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UoMs_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UoMs_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Warehouses_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReliefRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SpecificReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfRecipients = table.Column<int>(type: "int", nullable: false),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReliefSentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TargetDateToReceive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliefRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReliefRequests_ReliefSents_ReliefSentId",
                        column: x => x.ReliefSentId,
                        principalTable: "ReliefSents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefRequests_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReliefRequests_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefRequests_UserInfos_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UoMId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockItems_UoMs_UoMId",
                        column: x => x.UoMId,
                        principalTable: "UoMs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockItems_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockItems_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floors_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Floors_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Floors_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReliefReceiveds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcquisitionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliefReceiveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReliefReceiveds_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefReceiveds_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefReceiveds_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestAttachments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReliefRequestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestAttachments_ReliefRequests_ReliefRequestId",
                        column: x => x.ReliefRequestId,
                        principalTable: "ReliefRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestAttachments_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestAttachments_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReliefRequestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItems_ReliefRequests_ReliefRequestId",
                        column: x => x.ReliefRequestId,
                        principalTable: "ReliefRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestItems_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestItems_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReliefSentItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReliefSentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StockItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliefSentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReliefSentItems_ReliefSents_ReliefSentId",
                        column: x => x.ReliefSentId,
                        principalTable: "ReliefSents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefSentItems_StockItems_StockItemId",
                        column: x => x.StockItemId,
                        principalTable: "StockItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReliefSentItems_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefSentItems_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Racks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Racks_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Racks_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Racks_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Kits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonAssigned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    KitType = table.Column<int>(type: "int", nullable: false),
                    DatePacked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kits_Racks_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Racks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kits_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kits_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UomId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    FloorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockDetails_Racks_RackId",
                        column: x => x.RackId,
                        principalTable: "Racks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockDetails_ReliefReceiveds_BatchNo",
                        column: x => x.BatchNo,
                        principalTable: "ReliefReceiveds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockDetails_StockItems_StockItemId",
                        column: x => x.StockItemId,
                        principalTable: "StockItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockDetails_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockDetails_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KitComponents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitComponents_Kits_KitId",
                        column: x => x.KitId,
                        principalTable: "Kits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KitComponents_StockItems_StockItemId",
                        column: x => x.StockItemId,
                        principalTable: "StockItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KitComponents_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KitComponents_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackedReliefKits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DatePacked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FloorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackedReliefKits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackedReliefKits_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackedReliefKits_Kits_KitId",
                        column: x => x.KitId,
                        principalTable: "Kits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PackedReliefKits_Racks_RackId",
                        column: x => x.RackId,
                        principalTable: "Racks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PackedReliefKits_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PackedReliefKits_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReliefSentKits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReliefSentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    KitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliefSentKits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReliefSentKits_Kits_KitId",
                        column: x => x.KitId,
                        principalTable: "Kits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReliefSentKits_ReliefSents_ReliefSentId",
                        column: x => x.ReliefSentId,
                        principalTable: "ReliefSents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefSentKits_UserInfos_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefSentKits_UserInfos_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5c61e5f3-c6d1-431b-9435-547883956b58", null, "CSWD Administration Staff", "CSWD_ADMINISTRATION_STAFF" },
                    { "c35a31cb-082e-4d87-856a-baad6e0c59af", null, "CSWD Office Head", "CSWD_OFFICE_HEAD" },
                    { "fc17014d-6f12-45c4-8a42-873a442873d9", null, "Barangay Representative", "BARANGAY_REPRESENTATIVE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "934ec0ef-4c16-41a7-9832-ebde1d6cd23e", 0, "44ca2dde-b8ca-462f-b979-022132cabeae", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENkx43Zrg2/tiKZoCx7TL3mMQTIN8rXAZ3Z5WOldLFjUCL9lcdeAlUlZkA8Pyq9wRg==", null, false, "1a7220c9-93e2-47e4-a339-4f0d0eb395bc", false, "admin" });

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "Id", "AddedById", "Barangay", "DateCreated", "DateUpdated", "Email", "FirstName", "IsDeleted", "LastModifiedById", "LastName", "MiddleName", "Phone", "RoleId", "UserAuthId", "isArchived" },
                values: new object[] { "ACC_001", "ACC_001", "Karuhatan", new DateTime(2024, 12, 17, 1, 28, 56, 897, DateTimeKind.Local).AddTicks(1032), new DateTime(2024, 12, 17, 1, 28, 56, 897, DateTimeKind.Local).AddTicks(1033), "admin@gmail.com", "LakapSagap", false, null, "Admin", "Capstone", "09123456789", "5c61e5f3-c6d1-431b-9435-547883956b58", "934ec0ef-4c16-41a7-9832-ebde1d6cd23e", false });

            migrationBuilder.InsertData(
                table: "Attachments",
                columns: new[] { "Id", "AddedById", "DateCreated", "DateUpdated", "IsDeleted", "LastModifiedById", "Url", "UserId", "isArchived" },
                values: new object[] { "ATT_001", "ACC_001", new DateTime(2024, 12, 17, 1, 28, 56, 897, DateTimeKind.Local).AddTicks(995), new DateTime(2024, 12, 17, 1, 28, 56, 897, DateTimeKind.Local).AddTicks(1007), false, null, "wwwroot\\attachments\\default_user_image.png", "ACC_001", false });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AddedById",
                table: "Attachments",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_LastModifiedById",
                table: "Attachments",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_UserId",
                table: "Attachments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AddedById",
                table: "Categories",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LastModifiedById",
                table: "Categories",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_AddedById",
                table: "Floors",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_LastModifiedById",
                table: "Floors",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_WarehouseId",
                table: "Floors",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_KitComponents_AddedById",
                table: "KitComponents",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_KitComponents_KitId",
                table: "KitComponents",
                column: "KitId");

            migrationBuilder.CreateIndex(
                name: "IX_KitComponents_LastModifiedById",
                table: "KitComponents",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_KitComponents_StockItemId",
                table: "KitComponents",
                column: "StockItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_AddedById",
                table: "Kits",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_LastModifiedById",
                table: "Kits",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_LocationId",
                table: "Kits",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PackedReliefKits_AddedById",
                table: "PackedReliefKits",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_PackedReliefKits_FloorId",
                table: "PackedReliefKits",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PackedReliefKits_KitId",
                table: "PackedReliefKits",
                column: "KitId");

            migrationBuilder.CreateIndex(
                name: "IX_PackedReliefKits_LastModifiedById",
                table: "PackedReliefKits",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PackedReliefKits_RackId",
                table: "PackedReliefKits",
                column: "RackId");

            migrationBuilder.CreateIndex(
                name: "IX_Racks_AddedById",
                table: "Racks",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Racks_FloorId",
                table: "Racks",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Racks_LastModifiedById",
                table: "Racks",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefReceiveds_AddedById",
                table: "ReliefReceiveds",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefReceiveds_LastModifiedById",
                table: "ReliefReceiveds",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefReceiveds_WarehouseId",
                table: "ReliefReceiveds",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefRequests_AddedById",
                table: "ReliefRequests",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefRequests_LastModifiedById",
                table: "ReliefRequests",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefRequests_ReliefSentId",
                table: "ReliefRequests",
                column: "ReliefSentId",
                unique: true,
                filter: "[ReliefSentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefRequests_RequestedById",
                table: "ReliefRequests",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSentItems_AddedById",
                table: "ReliefSentItems",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSentItems_LastModifiedById",
                table: "ReliefSentItems",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSentItems_ReliefSentId",
                table: "ReliefSentItems",
                column: "ReliefSentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSentItems_StockItemId",
                table: "ReliefSentItems",
                column: "StockItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSentKits_AddedById",
                table: "ReliefSentKits",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSentKits_KitId",
                table: "ReliefSentKits",
                column: "KitId");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSentKits_LastModifiedById",
                table: "ReliefSentKits",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSentKits_ReliefSentId",
                table: "ReliefSentKits",
                column: "ReliefSentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSents_AddedById",
                table: "ReliefSents",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefSents_LastModifiedById",
                table: "ReliefSents",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAttachments_AddedById",
                table: "RequestAttachments",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAttachments_LastModifiedById",
                table: "RequestAttachments",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAttachments_ReliefRequestId",
                table: "RequestAttachments",
                column: "ReliefRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_AddedById",
                table: "RequestItems",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_LastModifiedById",
                table: "RequestItems",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_ReliefRequestId",
                table: "RequestItems",
                column: "ReliefRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_AddedById",
                table: "StockDetails",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_BatchNo",
                table: "StockDetails",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_LastModifiedById",
                table: "StockDetails",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_RackId",
                table: "StockDetails",
                column: "RackId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_StockItemId",
                table: "StockDetails",
                column: "StockItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_AddedById",
                table: "StockItems",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_CategoryId",
                table: "StockItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_LastModifiedById",
                table: "StockItems",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_UoMId",
                table: "StockItems",
                column: "UoMId");

            migrationBuilder.CreateIndex(
                name: "IX_UoMs_AddedById",
                table: "UoMs",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_UoMs_LastModifiedById",
                table: "UoMs",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_AddedById",
                table: "UserInfos",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_LastModifiedById",
                table: "UserInfos",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_RoleId",
                table: "UserInfos",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserAuthId",
                table: "UserInfos",
                column: "UserAuthId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_AddedById",
                table: "Warehouses",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_LastModifiedById",
                table: "Warehouses",
                column: "LastModifiedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "KitComponents");

            migrationBuilder.DropTable(
                name: "PackedReliefKits");

            migrationBuilder.DropTable(
                name: "ReliefSentItems");

            migrationBuilder.DropTable(
                name: "ReliefSentKits");

            migrationBuilder.DropTable(
                name: "RequestAttachments");

            migrationBuilder.DropTable(
                name: "RequestItems");

            migrationBuilder.DropTable(
                name: "StockDetails");

            migrationBuilder.DropTable(
                name: "Kits");

            migrationBuilder.DropTable(
                name: "ReliefRequests");

            migrationBuilder.DropTable(
                name: "ReliefReceiveds");

            migrationBuilder.DropTable(
                name: "StockItems");

            migrationBuilder.DropTable(
                name: "Racks");

            migrationBuilder.DropTable(
                name: "ReliefSents");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "UoMs");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
