using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAKAPSAGAP.Services.Migrations
{
    /// <inheritdoc />
    public partial class adjustmentcommonmodel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_UserInfo_UserId",
                table: "Attachment");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "Warehouse",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "Warehouse",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "UserInfo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "UserInfo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "StockType",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "StockType",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StockTypeId",
                table: "StockItem",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StockCategoryId",
                table: "StockItem",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "StockItem",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "StockItem",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "StockDetail",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "StockDetail",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "StockCategory",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "StockCategory",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "ReliefReceived",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "ReliefReceived",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "Attachment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "Attachment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserInfoId",
                table: "Attachment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_AddedById",
                table: "Warehouse",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_LastModifiedById",
                table: "Warehouse",
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
                name: "IX_StockType_AddedById",
                table: "StockType",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockType_LastModifiedById",
                table: "StockType",
                column: "LastModifiedById");

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
                name: "IX_StockItem_StockTypeId",
                table: "StockItem",
                column: "StockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_AddedById",
                table: "StockDetail",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetail_LastModifiedById",
                table: "StockDetail",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockCategory_AddedById",
                table: "StockCategory",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockCategory_LastModifiedById",
                table: "StockCategory",
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
                name: "IX_Attachment_AddedById",
                table: "Attachment",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_LastModifiedById",
                table: "Attachment",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_UserInfoId",
                table: "Attachment",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_UserInfo_AddedById",
                table: "Attachment",
                column: "AddedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_UserInfo_LastModifiedById",
                table: "Attachment",
                column: "LastModifiedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_UserInfo_UserId",
                table: "Attachment",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_UserInfo_UserInfoId",
                table: "Attachment",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReliefReceived_UserInfo_AddedById",
                table: "ReliefReceived",
                column: "AddedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReliefReceived_UserInfo_LastModifiedById",
                table: "ReliefReceived",
                column: "LastModifiedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockCategory_UserInfo_AddedById",
                table: "StockCategory",
                column: "AddedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockCategory_UserInfo_LastModifiedById",
                table: "StockCategory",
                column: "LastModifiedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockDetail_UserInfo_AddedById",
                table: "StockDetail",
                column: "AddedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockDetail_UserInfo_LastModifiedById",
                table: "StockDetail",
                column: "LastModifiedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItem_StockCategory_StockCategoryId",
                table: "StockItem",
                column: "StockCategoryId",
                principalTable: "StockCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItem_StockType_StockTypeId",
                table: "StockItem",
                column: "StockTypeId",
                principalTable: "StockType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItem_UserInfo_AddedById",
                table: "StockItem",
                column: "AddedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItem_UserInfo_LastModifiedById",
                table: "StockItem",
                column: "LastModifiedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockType_UserInfo_AddedById",
                table: "StockType",
                column: "AddedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockType_UserInfo_LastModifiedById",
                table: "StockType",
                column: "LastModifiedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_UserInfo_AddedById",
                table: "UserInfo",
                column: "AddedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_UserInfo_LastModifiedById",
                table: "UserInfo",
                column: "LastModifiedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_UserInfo_AddedById",
                table: "Warehouse",
                column: "AddedById",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_UserInfo_LastModifiedById",
                table: "Warehouse",
                column: "LastModifiedById",
                principalTable: "UserInfo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_UserInfo_AddedById",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_UserInfo_LastModifiedById",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_UserInfo_UserId",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_UserInfo_UserInfoId",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReliefReceived_UserInfo_AddedById",
                table: "ReliefReceived");

            migrationBuilder.DropForeignKey(
                name: "FK_ReliefReceived_UserInfo_LastModifiedById",
                table: "ReliefReceived");

            migrationBuilder.DropForeignKey(
                name: "FK_StockCategory_UserInfo_AddedById",
                table: "StockCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_StockCategory_UserInfo_LastModifiedById",
                table: "StockCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_StockDetail_UserInfo_AddedById",
                table: "StockDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_StockDetail_UserInfo_LastModifiedById",
                table: "StockDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItem_StockCategory_StockCategoryId",
                table: "StockItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItem_StockType_StockTypeId",
                table: "StockItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItem_UserInfo_AddedById",
                table: "StockItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItem_UserInfo_LastModifiedById",
                table: "StockItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockType_UserInfo_AddedById",
                table: "StockType");

            migrationBuilder.DropForeignKey(
                name: "FK_StockType_UserInfo_LastModifiedById",
                table: "StockType");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_UserInfo_AddedById",
                table: "UserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_UserInfo_LastModifiedById",
                table: "UserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_UserInfo_AddedById",
                table: "Warehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_UserInfo_LastModifiedById",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_AddedById",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_LastModifiedById",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_AddedById",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_LastModifiedById",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_StockType_AddedById",
                table: "StockType");

            migrationBuilder.DropIndex(
                name: "IX_StockType_LastModifiedById",
                table: "StockType");

            migrationBuilder.DropIndex(
                name: "IX_StockItem_AddedById",
                table: "StockItem");

            migrationBuilder.DropIndex(
                name: "IX_StockItem_LastModifiedById",
                table: "StockItem");

            migrationBuilder.DropIndex(
                name: "IX_StockItem_StockCategoryId",
                table: "StockItem");

            migrationBuilder.DropIndex(
                name: "IX_StockItem_StockTypeId",
                table: "StockItem");

            migrationBuilder.DropIndex(
                name: "IX_StockDetail_AddedById",
                table: "StockDetail");

            migrationBuilder.DropIndex(
                name: "IX_StockDetail_LastModifiedById",
                table: "StockDetail");

            migrationBuilder.DropIndex(
                name: "IX_StockCategory_AddedById",
                table: "StockCategory");

            migrationBuilder.DropIndex(
                name: "IX_StockCategory_LastModifiedById",
                table: "StockCategory");

            migrationBuilder.DropIndex(
                name: "IX_ReliefReceived_AddedById",
                table: "ReliefReceived");

            migrationBuilder.DropIndex(
                name: "IX_ReliefReceived_LastModifiedById",
                table: "ReliefReceived");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_AddedById",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_LastModifiedById",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_UserInfoId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Attachment");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "Warehouse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "Warehouse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "UserInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "UserInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "StockType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "StockType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StockTypeId",
                table: "StockItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StockCategoryId",
                table: "StockItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "StockItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "StockItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "StockDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "StockDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "StockCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "StockCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "ReliefReceived",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "ReliefReceived",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedById",
                table: "Attachment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddedById",
                table: "Attachment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_UserInfo_UserId",
                table: "Attachment",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
