using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserSellerKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_AspNetUsers_UserId",
                table: "Sellers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sellers",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers",
                newName: "IX_Sellers_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_AspNetUsers_ApplicationUserId",
                table: "Sellers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_AspNetUsers_ApplicationUserId",
                table: "Sellers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Sellers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sellers_ApplicationUserId",
                table: "Sellers",
                newName: "IX_Sellers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_AspNetUsers_UserId",
                table: "Sellers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
