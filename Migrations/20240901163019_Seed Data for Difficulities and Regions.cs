using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExploreAPIs.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataforDifficulitiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2fdfee01-7e11-465b-af07-566fb4d9996e"), "Hard" },
                    { new Guid("66c35d15-6073-41af-aef2-9f0e9e568483"), "Easy" },
                    { new Guid("cff873cd-312b-4f69-a8ca-dfbf1f3b1107"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImgUrl" },
                values: new object[,]
                {
                    { new Guid("1d67382d-a7a9-4cfa-ab26-911c2d6ff6cc"), "CH", "Chicago", "Chicago Pic.png" },
                    { new Guid("384ddf4b-ce21-4858-b428-df09ca54181d"), "HO", "Houston", "Houston Pic.png" },
                    { new Guid("d61f51dd-6c63-42d8-939c-659e43149bde"), "NY", "New York", "New York Pic.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("2fdfee01-7e11-465b-af07-566fb4d9996e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("66c35d15-6073-41af-aef2-9f0e9e568483"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("cff873cd-312b-4f69-a8ca-dfbf1f3b1107"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1d67382d-a7a9-4cfa-ab26-911c2d6ff6cc"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("384ddf4b-ce21-4858-b428-df09ca54181d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d61f51dd-6c63-42d8-939c-659e43149bde"));
        }
    }
}
