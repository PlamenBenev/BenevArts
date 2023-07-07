using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssetSellers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Sellers_SellerId",
                table: "Assets");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Sellers_SellerId",
                table: "Assets",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Sellers_SellerId",
                table: "Assets");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Sellers_SellerId",
                table: "Assets",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
