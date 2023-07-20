using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class SellerApplicationKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "SellersApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SellersApplications_ApplicationUserId",
                table: "SellersApplications",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellersApplications_AspNetUsers_ApplicationUserId",
                table: "SellersApplications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellersApplications_AspNetUsers_ApplicationUserId",
                table: "SellersApplications");

            migrationBuilder.DropIndex(
                name: "IX_SellersApplications_ApplicationUserId",
                table: "SellersApplications");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "SellersApplications");
        }
    }
}
