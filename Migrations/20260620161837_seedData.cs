using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToCarAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "CreatedAt", "ImageUrl", "IsActive", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x200", false, "عروض الصيف" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x200", false, "تخفيضات قطع الغيار" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ImageUrl", "Name", "Title" },
                values: new object[,]
                {
                    { 1, "https://via.placeholder.com/150", "تشكيلة السيارات", "" },
                    { 2, "https://via.placeholder.com/150", "قطع الغيار", "" },
                    { 3, "https://via.placeholder.com/150", "الإكسسوارات", "" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Description", "DistributingCompany", "ImageUrl", "PartCode", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, "سيارة مريحة واقتصادية", "", "https://via.placeholder.com/150", "", 25000m, "تويوتا كامري" },
                    { 2, 1, "سيارة اقتصادية وموثوقة", "", "https://via.placeholder.com/150", "", 20000m, "هوندا سيفيك" },
                    { 3, 2, "فلتر زيت عالي الجودة", "", "https://via.placeholder.com/150", "", 15m, "فلتر زيت" },
                    { 4, 2, "طفاية هواء أصلية", "", "https://via.placeholder.com/150", "", 50m, "طفاية هواء" },
                    { 5, 3, "غطاء مقود جلدي فاخر", "", "https://via.placeholder.com/150", "", 30m, "غطاء مقود" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banners",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
