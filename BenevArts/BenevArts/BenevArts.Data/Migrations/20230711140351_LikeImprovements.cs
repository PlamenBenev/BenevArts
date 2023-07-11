using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class LikeImprovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Assets_AssetID",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "AssetID",
                table: "Likes",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_AssetID",
                table: "Likes",
                newName: "IX_Likes_AssetId");

            migrationBuilder.AddColumn<bool>(
                name: "IsLikedByCurrentUser",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Assets_AssetId",
                table: "Likes",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Assets_AssetId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "IsLikedByCurrentUser",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "Likes",
                newName: "AssetID");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_AssetId",
                table: "Likes",
                newName: "IX_Likes_AssetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Assets_AssetID",
                table: "Likes",
                column: "AssetID",
                principalTable: "Assets",
                principalColumn: "Id");
        }
    }
}
