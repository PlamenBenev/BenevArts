using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageForCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                column: "Image",
                value: "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categories");
        }
    }
}
