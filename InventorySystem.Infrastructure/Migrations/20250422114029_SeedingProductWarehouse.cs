using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventorySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingProductWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InventoryMovements",
                columns: new[] { "InventoryMovementId", "MovementDate", "MovementType", "ProductId", "QuantityChanged", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("a1a1a1a1-0000-0000-0000-000000000001"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Inbound", new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), 50, new Guid("aaaa1111-1111-1111-1111-111111111111") },
                    { new Guid("b2b2b2b2-0000-0000-0000-000000000002"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Transfer", new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), 30, new Guid("dddd4444-4444-4444-4444-444444444444") },
                    { new Guid("c3c3c3c3-0000-0000-0000-000000000003"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Outbound", new Guid("55555555-eeee-eeee-eeee-555555555555"), 30, new Guid("aaaa1111-1111-1111-1111-111111111111") },
                    { new Guid("d4d4d4d4-0000-0000-0000-000000000004"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Transfer", new Guid("33333333-cccc-cccc-cccc-333333333333"), 30, new Guid("eeee5555-5555-5555-5555-555555555555") },
                    { new Guid("e5e5e5e5-0000-0000-0000-000000000005"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Outbound", new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), 30, new Guid("cccc3333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.InsertData(
                table: "ProductWarehouses",
                columns: new[] { "ProductWarehouseId", "ProductId", "Quantity", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("0021f1f1-0000-0000-0000-000000000001"), new Guid("33333333-cccc-cccc-cccc-333333333333"), 200, new Guid("cccc3333-3333-3333-3333-333333333333") },
                    { new Guid("23f1f1f1-0020-0000-0000-000000000001"), new Guid("44444444-dddd-dddd-dddd-444444444444"), 30, new Guid("dddd4444-4444-4444-4444-444444444444") },
                    { new Guid("f1f1f1f1-0000-0000-0000-000000000001"), new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), 150, new Guid("aaaa1111-1111-1111-1111-111111111111") },
                    { new Guid("f9dc1f1f-0000-0000-0000-000000000001"), new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), 80, new Guid("bbbb2222-2222-2222-2222-222222222222") },
                    { new Guid("fc1b1f1f-0000-0000-0000-000000000001"), new Guid("55555555-eeee-eeee-eeee-555555555555"), 500, new Guid("eeee5555-5555-5555-5555-555555555555") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InventoryMovements",
                keyColumn: "InventoryMovementId",
                keyValue: new Guid("a1a1a1a1-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "InventoryMovements",
                keyColumn: "InventoryMovementId",
                keyValue: new Guid("b2b2b2b2-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "InventoryMovements",
                keyColumn: "InventoryMovementId",
                keyValue: new Guid("c3c3c3c3-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "InventoryMovements",
                keyColumn: "InventoryMovementId",
                keyValue: new Guid("d4d4d4d4-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "InventoryMovements",
                keyColumn: "InventoryMovementId",
                keyValue: new Guid("e5e5e5e5-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "ProductWarehouses",
                keyColumn: "ProductWarehouseId",
                keyValue: new Guid("0021f1f1-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductWarehouses",
                keyColumn: "ProductWarehouseId",
                keyValue: new Guid("23f1f1f1-0020-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductWarehouses",
                keyColumn: "ProductWarehouseId",
                keyValue: new Guid("f1f1f1f1-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductWarehouses",
                keyColumn: "ProductWarehouseId",
                keyValue: new Guid("f9dc1f1f-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductWarehouses",
                keyColumn: "ProductWarehouseId",
                keyValue: new Guid("fc1b1f1f-0000-0000-0000-000000000001"));
        }
    }
}
