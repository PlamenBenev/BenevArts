using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedCategoryImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://360view.hum3d.com/zoom/Animals/Allosaurus_1000_0001.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "https://img2.cgtrader.com/items/1949905/2aef8f1d69/loom-machine-3d-model-max-fbx.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://duckduckgo.com/?q=3d+animal+quad+resolution+image&iar=images&iax=images&ia=images&iai=https%3A%2F%2F360view.hum3d.com%2Fzoom%2FAnimals%2FAllosaurus_1000_0001.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "https://duckduckgo.com/?q=Industrial+3d+model+1500x1500+resolution+image&iar=images&iax=images&ia=images&iai=https%3A%2F%2Fstatic.turbosquid.com%2FPreview%2F001237%2F427%2FDZ%2Fconstruction-building-industrial-3D-model_DHQ.jpg");
        }
    }
}
