using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssetDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Animated",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CGIModel",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LowPoly",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Materials",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PBR",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Rigged",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Textures",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UVUnwrapped",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ZipFileName",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Animated",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "CGIModel",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LowPoly",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Materials",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PBR",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Rigged",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Textures",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "UVUnwrapped",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ZipFileName",
                table: "Assets");
        }
    }
}
