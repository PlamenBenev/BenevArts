using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedCategoryImageSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://www.renderhub.com/zyed/container-office-building/container-office-building.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://www.renderhub.com/mm2endra/complete-house-exterior/complete-house-exterior.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "Image",
                value: "https://www.renderhub.com/bsw2142/kitchen-appliances/kitchen-appliances.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "Image",
                value: "https://www.renderhub.com/sky3dstudios69/construction-vehicle-001/construction-vehicle-001.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://1.bp.blogspot.com/-SWj-15zct78/UUNzgK9hqHI/AAAAAAAAAbs/xn0OfeGyNDw/s1600/3D+Architectural+Designs+(21)+HD+Latest+Pictures+Photos+Wallpapers.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://www.cleanpix.com.au/images/1751_EXT_Hero_View2_D.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "Image",
                value: "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.3dvalley.com%2Fuser%2Fpages%2F02.3d-models%2F05.household%2Fbathtub.jpg&f=1&nofb=1&ipt=81746dee5b141a5068a68673f81ff7b8e3702e2250c7e3988666d26bd84d6e6f&ipo=images");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "Image",
                value: "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.m2models.hr%2FPictures%2Fgalerija%2FThumbnails%2FSlike%2FV43.JPG&f=1&nofb=1&ipt=9d73ada32b967c128a4595983059f033a9cfc364d2b1219d5cd358396a648637&ipo=images");
        }
    }
}
