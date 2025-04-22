using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventorySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingSupplierEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "Address", "ContactEmail", "ContactInfo", "Name" },
                values: new object[,]
                {
                    { new Guid("1111aaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), " 25, Okemagba Avenue, Agege, Lagos State", "techsupplies@example.com", "08012345678", "Tech Supplies Co." },
                    { new Guid("2222bbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), " Ikeja City Mall, Abdulrazak Nwabude Drive", "bookworld@example.com", "08123456789", "Book World" },
                    { new Guid("3333cccc-cccc-cccc-cccc-cccccccccccc"), "10, Broad Street, Lagos Island", "fashionhub@example.com", "09012345678", "Fashion Hub" },
                    { new Guid("4444dddd-dddd-dddd-dddd-dddddddddddd"), "Km 19, Lekki-Epe Expressway, Lekki,", "furnistore@example.com", "08087654321", "FurniStore" },
                    { new Guid("5555eeee-eeee-eeee-eeee-eeeeeeeeeeee"), "21, Ibadan Street, Surulere,", "grocerymart@example.com", "08111223344", "Grocery Mart" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: new Guid("1111aaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: new Guid("2222bbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: new Guid("3333cccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: new Guid("4444dddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: new Guid("5555eeee-eeee-eeee-eeee-eeeeeeeeeeee"));
        }
    }
}
