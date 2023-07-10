using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class Comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Assets_AssetID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AssetID",
                table: "Comments",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AssetID",
                table: "Comments",
                newName: "IX_Comments_AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Assets_AssetId",
                table: "Comments",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Assets_AssetId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "Comments",
                newName: "AssetID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AssetId",
                table: "Comments",
                newName: "IX_Comments_AssetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Assets_AssetID",
                table: "Comments",
                column: "AssetID",
                principalTable: "Assets",
                principalColumn: "Id");
        }
    }
}
