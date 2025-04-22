using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventorySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuditLogs",
                columns: new[] { "AuditLogId", "Action", "CreatedAt", "Details", "PerformedBy" },
                values: new object[,]
                {
                    { new Guid("abcabc01-0000-0000-0000-000000000001"), "Product Added", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Product was added", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("abcabc02-0000-0000-0000-000000000002"), "Warehouse Created", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Warehouse created", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("abcabc03-0000-0000-0000-000000000003"), "Inventory Movement", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "InventoryMovement recored", new Guid("11111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "Name", "Price", "SupplierId" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Smartphone", 499.99m, new Guid("1111aaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Novel", 19.99m, new Guid("2222bbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("33333333-cccc-cccc-cccc-333333333333"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "T-shirt", 15.99m, new Guid("3333cccc-cccc-cccc-cccc-cccccccccccc") },
                    { new Guid("44444444-dddd-dddd-dddd-444444444444"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sofa", 799.99m, new Guid("4444dddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("55555555-eeee-eeee-eeee-555555555555"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cereal", 3.49m, new Guid("5555eeee-eeee-eeee-eeee-eeeeeeeeeeee") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuditLogs",
                keyColumn: "AuditLogId",
                keyValue: new Guid("abcabc01-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "AuditLogs",
                keyColumn: "AuditLogId",
                keyValue: new Guid("abcabc02-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "AuditLogs",
                keyColumn: "AuditLogId",
                keyValue: new Guid("abcabc03-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("11111111-aaaa-aaaa-aaaa-111111111111"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-222222222222"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("33333333-cccc-cccc-cccc-333333333333"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("44444444-dddd-dddd-dddd-444444444444"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("55555555-eeee-eeee-eeee-555555555555"));
        }
    }
}
