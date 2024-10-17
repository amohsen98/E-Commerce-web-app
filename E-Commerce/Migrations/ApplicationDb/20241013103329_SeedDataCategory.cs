using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class SeedDataCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "CreatedTime", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 13, 13, 33, 28, 747, DateTimeKind.Local).AddTicks(938), "Desc1", "Category 1" },
                    { 2, new DateTime(2024, 10, 13, 13, 33, 28, 747, DateTimeKind.Local).AddTicks(986), "Desc2", "Category 2" },
                    { 3, new DateTime(2024, 10, 13, 13, 33, 28, 747, DateTimeKind.Local).AddTicks(989), "Desc3", "Category 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
