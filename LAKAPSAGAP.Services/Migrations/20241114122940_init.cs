using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAuthId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfo_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInfo_AspNetUsers_UserAuthId",
                        column: x => x.UserAuthId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInfo_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserInfo_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserInfoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_StockCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCategory_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockCategory_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UoM",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_UoM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UoM_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UoM_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
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
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Warehouse_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockCategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_StockItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockItem_StockCategory_StockCategoryId",
                        column: x => x.StockCategoryId,
                        principalTable: "StockCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockItem_UoM_UoMId",
                        column: x => x.UoMId,
                        principalTable: "UoM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockItem_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockItem_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Floor",
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
                    table.PrimaryKey("PK_Floor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floor_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Floor_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Floor_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReliefReceived",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReliefType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruckPlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliefReceived", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReliefReceived_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefReceived_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReliefReceived_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rack",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FloorId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rack_Floor_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rack_Floor_FloorId1",
                        column: x => x.FloorId1,
                        principalTable: "Floor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rack_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rack_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UoMId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FloorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReliefReceivedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockDetail_Floor_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockDetail_Rack_RackId",
                        column: x => x.RackId,
                        principalTable: "Rack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockDetail_ReliefReceived_BatchNumber",
                        column: x => x.BatchNumber,
                        principalTable: "ReliefReceived",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockDetail_ReliefReceived_ReliefReceivedId",
                        column: x => x.ReliefReceivedId,
                        principalTable: "ReliefReceived",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockDetail_StockCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "StockCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockDetail_StockItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "StockItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockDetail_UoM_UoMId",
                        column: x => x.UoMId,
                        principalTable: "UoM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockDetail_UserInfo_AddedById",
                        column: x => x.AddedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockDetail_UserInfo_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "UserInfo",
                        principalColumn: "Id");
                });

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
                name: "IX_Attachment_AddedById",
                table: "Attachment",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_LastModifiedById",
                table: "Attachment",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_UserId",
                table: "Attachment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_UserInfoId",
                table: "Attachment",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_AddedById",
                table: "Floor",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_LastModifiedById",
                table: "Floor",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_WarehouseId",
                table: "Floor",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_AddedById",
                table: "Rack",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_FloorId",
                table: "Rack",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_FloorId1",
                table: "Rack",
                column: "FloorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_LastModifiedById",
                table: "Rack",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefReceived_AddedById",
                table: "ReliefReceived",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefReceived_LastModifiedById",
                table: "ReliefReceived",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReliefReceived_WarehouseId",
                table: "ReliefReceived",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCategory_AddedById",
                table: "StockCategory",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockCategory_LastModifiedById",
                table: "StockCategory",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_AddedById",
                table: "StockDetail",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_BatchNumber",
                table: "StockDetail",
                column: "BatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_CategoryId",
                table: "StockDetail",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_FloorId",
                table: "StockDetail",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_ItemId",
                table: "StockDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_LastModifiedById",
                table: "StockDetail",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_RackId",
                table: "StockDetail",
                column: "RackId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_ReliefReceivedId",
                table: "StockDetail",
                column: "ReliefReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_UoMId",
                table: "StockDetail",
                column: "UoMId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItem_AddedById",
                table: "StockItem",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockItem_LastModifiedById",
                table: "StockItem",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockItem_StockCategoryId",
                table: "StockItem",
                column: "StockCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItem_UoMId",
                table: "StockItem",
                column: "UoMId");

            migrationBuilder.CreateIndex(
                name: "IX_UoM_AddedById",
                table: "UoM",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_UoM_LastModifiedById",
                table: "UoM",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_AddedById",
                table: "UserInfo",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_LastModifiedById",
                table: "UserInfo",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_RoleId",
                table: "UserInfo",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserAuthId",
                table: "UserInfo",
                column: "UserAuthId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_AddedById",
                table: "Warehouse",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_LastModifiedById",
                table: "Warehouse",
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
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "StockDetail");

            migrationBuilder.DropTable(
                name: "Rack");

            migrationBuilder.DropTable(
                name: "ReliefReceived");

            migrationBuilder.DropTable(
                name: "StockItem");

            migrationBuilder.DropTable(
                name: "Floor");

            migrationBuilder.DropTable(
                name: "StockCategory");

            migrationBuilder.DropTable(
                name: "UoM");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
