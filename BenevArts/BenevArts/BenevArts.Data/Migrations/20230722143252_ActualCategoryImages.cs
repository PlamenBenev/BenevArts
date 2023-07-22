using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenevArts.Data.Migrations
{
    /// <inheritdoc />
    public partial class ActualCategoryImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: 5,
                column: "Image",
                value: "https://mir-s3-cdn-cf.behance.net/project_modules/1400/4eddca30402501.56210b527c788.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "Image",
                value: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fget.wallhere.com%2Fphoto%2F1500x1500-px-GT1-LE-lemans-mans-Nissan-R390-race-racing-supercar-1672607.jpg&f=1&nofb=1&ipt=62e16a3e4f3cfcf3ab999db9b598e17e6ce3a49cea0cbf88e375afbbba3206c1&ipo=images");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "Image",
                value: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fstatic.zerochan.net%2FHrothgar.full.2600742.png&f=1&nofb=1&ipt=f36ae45a8caf6f2980beb52a695e6db13dc4edc2340d9793110d24f36794f80d&ipo=images");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "Image",
                value: "https://static.turbosquid.com/Preview/2019/07/17__13_46_54/Signature.jpgD810065D-0F62-4EBB-847C-AA57E3F7D50ADefault.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "Image",
                value: "https://static.turbosquid.com/Preview/2020/07/20__00_13_48/1f.pngFE53EBFF-5100-497F-8874-49F155AC925BDefault.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "Image",
                value: "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.3dvalley.com%2Fuser%2Fpages%2F02.3d-models%2F05.household%2Fbathtub.jpg&f=1&nofb=1&ipt=81746dee5b141a5068a68673f81ff7b8e3702e2250c7e3988666d26bd84d6e6f&ipo=images");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "https://duckduckgo.com/?q=Industrial+3d+model+1500x1500+resolution+image&iar=images&iax=images&ia=images&iai=https%3A%2F%2Fstatic.turbosquid.com%2FPreview%2F001237%2F427%2FDZ%2Fconstruction-building-industrial-3D-model_DHQ.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                column: "Image",
                value: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2F736x%2Fee%2Fd1%2F33%2Feed133e60e4646d749c0a7e87da7f9e8.jpg&f=1&nofb=1&ipt=bc48b05a520757ee97c69e1e8c9f79e06ee8eae82203792e334bf3556edef1d1&ipo=images");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "Image",
                value: "https://cdn.shopify.com/s/files/1/2191/8173/products/discovery-space-shuttle-nasa-3d-models-_3_1024x1024.jpg?v=1504007478");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                column: "Image",
                value: "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.m2models.hr%2FPictures%2Fgalerija%2FThumbnails%2FSlike%2FV43.JPG&f=1&nofb=1&ipt=9d73ada32b967c128a4595983059f033a9cfc364d2b1219d5cd358396a648637&ipo=images");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                column: "Image",
                value: "https://3dlenta.com/components/com_virtuemart/shop_image/product/Watercraft_003_4ab8a73fa534b.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                column: "Image",
                value: "https://static.turbosquid.com/Preview/2014/05/16__03_52_30/MATV_Grad_Cam01.jpg217fdd9e-8a2c-49ba-9048-aeb9a1311120Zoom.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
