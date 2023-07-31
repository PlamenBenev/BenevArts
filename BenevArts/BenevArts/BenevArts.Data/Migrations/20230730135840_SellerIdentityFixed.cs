using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class SellerIdentityFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_UserID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Assets_AssetID",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sellers",
                newName: "StoreName");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Purchases",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AssetID",
                table: "Purchases",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_UserID",
                table: "Purchases",
                newName: "IX_Purchases_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_AssetID",
                table: "Purchases",
                newName: "IX_Purchases_AssetId");

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "Sellers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Assets_AssetId",
                table: "Purchases",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Assets_AssetId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "Sellers");

            migrationBuilder.RenameColumn(
                name: "StoreName",
                table: "Sellers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Purchases",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "Purchases",
                newName: "AssetID");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                newName: "IX_Purchases_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_AssetId",
                table: "Purchases",
                newName: "IX_Purchases_AssetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_UserID",
                table: "Purchases",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Assets_AssetID",
                table: "Purchases",
                column: "AssetID",
                principalTable: "Assets",
                principalColumn: "Id");
        }
    }
}
