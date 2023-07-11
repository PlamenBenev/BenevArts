using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class LikeFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLikedByCurrentUser",
                table: "Assets");

            migrationBuilder.AddColumn<bool>(
                name: "IsLikedByCurrentUser",
                table: "Likes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLikedByCurrentUser",
                table: "Likes");

            migrationBuilder.AddColumn<bool>(
                name: "IsLikedByCurrentUser",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
