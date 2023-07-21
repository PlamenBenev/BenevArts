using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "SellersApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "SellersApplications");
        }
    }
}
