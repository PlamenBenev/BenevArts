using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "SellersApplications",
                newName: "StorePhone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SellersApplications",
                newName: "StoreName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "SellersApplications",
                newName: "StoreEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorePhone",
                table: "SellersApplications",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "StoreName",
                table: "SellersApplications",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StoreEmail",
                table: "SellersApplications",
                newName: "Email");
        }
    }
}
