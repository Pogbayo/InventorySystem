using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventorySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingWarehouses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "WarehouseId", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaa1111-1111-1111-1111-111111111111"), "Ikeja", "Lagos Warehouse" },
                    { new Guid("bbbb2222-2222-2222-2222-222222222222"), "Gwarinpa", "Abuja Warehouse" },
                    { new Guid("cccc3333-3333-3333-3333-333333333333"), "D-Line", "Port Harcourt Warehouse" },
                    { new Guid("dddd4444-4444-4444-4444-444444444444"), "Kofar Ruwa", "Kano Warehouse" },
                    { new Guid("eeee5555-5555-5555-5555-555555555555"), "Abeokuta", "Ogun Warehouse" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "WarehouseId",
                keyValue: new Guid("aaaa1111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "WarehouseId",
                keyValue: new Guid("bbbb2222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "WarehouseId",
                keyValue: new Guid("cccc3333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "WarehouseId",
                keyValue: new Guid("dddd4444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "WarehouseId",
                keyValue: new Guid("eeee5555-5555-5555-5555-555555555555"));
        }
    }
}
