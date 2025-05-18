using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Persistence
{
    public class InventorySystemDb : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public InventorySystemDb(DbContextOptions<InventorySystemDb> options) : base(options) { }

        public DbSet<AuditLog> AuditLogs { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        //private static readonly string user1PasswordHash = "AQAAAAIAAYagAAAAEO6qtAuWzYJjR/7w1l6S7P9kJ5W8qX1mB2nC3dE4fG5hH6iI7jJ8kL9mN==";
        //private static readonly string user2PasswordHash = "AQAAAAIAAYagAAAAEIxqZfQ3P4oN5mM6l7kJ8iH9gF8eD7cB6aA5nM4oL3jJ2kK1iH0gF9dE==";


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<InventoryMovement>()
            //        .Property(e => e.MovementType)
            //        .HasConversion<string>();

            //    //var hasher = new PasswordHasher<ApplicationUser>();

            //    //string hashedPasswordUser1 = hasher.HashPassword(new ApplicationUser(), "Password123!");
            //    //string hashedPasswordUser2 = hasher.HashPassword(new ApplicationUser(), "Password123!");


            //    var testUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            //    //var hasher = new PasswordHasher<ApplicationUser>();


            //    var electronicsCatId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            //    var booksCatId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            //    var clothingCatId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            //    var furnitureCatId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            //    var foodCatId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            //    var product1Id = Guid.Parse("11111111-aaaa-aaaa-aaaa-111111111111");
            //    var product2Id = Guid.Parse("22222222-bbbb-bbbb-bbbb-222222222222");
            //    var product3Id = Guid.Parse("33333333-cccc-cccc-cccc-333333333333");
            //    var product4Id = Guid.Parse("44444444-dddd-dddd-dddd-444444444444");
            //    var product5Id = Guid.Parse("55555555-eeee-eeee-eeee-555555555555");

            //    var warehouse1Id = Guid.Parse("aaaa1111-1111-1111-1111-111111111111");
            //    var warehouse2Id = Guid.Parse("bbbb2222-2222-2222-2222-222222222222");
            //    var warehouse3Id = Guid.Parse("cccc3333-3333-3333-3333-333333333333");
            //    var warehouse4Id = Guid.Parse("dddd4444-4444-4444-4444-444444444444");
            //    var warehouse5Id = Guid.Parse("eeee5555-5555-5555-5555-555555555555");

            //    var supplier1Id = Guid.Parse("1111aaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            //    var supplier2Id = Guid.Parse("2222bbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            //    var supplier3Id = Guid.Parse("3333cccc-cccc-cccc-cccc-cccccccccccc");
            //    var supplier4Id = Guid.Parse("4444dddd-dddd-dddd-dddd-dddddddddddd");
            //    var supplier5Id = Guid.Parse("5555eeee-eeee-eeee-eeee-eeeeeeeeeeee");

            //    var inventoryMovement1Id = Guid.Parse("a1a1a1a1-0000-0000-0000-000000000001");
            //    var inventoryMovement2Id = Guid.Parse("b2b2b2b2-0000-0000-0000-000000000002");
            //    var inventoryMovement3Id = Guid.Parse("c3c3c3c3-0000-0000-0000-000000000003");
            //    var inventoryMovement4Id = Guid.Parse("d4d4d4d4-0000-0000-0000-000000000004");
            //    var inventoryMovement5Id = Guid.Parse("e5e5e5e5-0000-0000-0000-000000000005");

            //    var auditLog1Id = Guid.Parse("abcabc01-0000-0000-0000-000000000001");
            //    var auditLog2Id = Guid.Parse("abcabc02-0000-0000-0000-000000000002");
            //    var auditLog3Id = Guid.Parse("abcabc03-0000-0000-0000-000000000003");
            //    var auditLog4Id = Guid.Parse("abcabc04-0000-0000-0000-000000000004");
            //    var auditLog5Id = Guid.Parse("abcabc05-0000-0000-0000-000000000005");

            //    var productWarehouse1Id = Guid.Parse("f1f1f1f1-0000-0000-0000-000000000001");
            //    var productWarehouse2Id = Guid.Parse("f9dc1f1f-0000-0000-0000-000000000001");
            //    var productWarehouse3Id = Guid.Parse("0021f1f1-0000-0000-0000-000000000001");
            //    var productWarehouse4Id = Guid.Parse("23f1f1f1-0020-0000-0000-000000000001");
            //    var productWarehouse5Id = Guid.Parse("fc1b1f1f-0000-0000-0000-000000000001");

            //    //var user1Id = "12345678-1234-1234-1234-123456789012";
            //    //var user2Id = "23456789-2345-2345-2345-234567890123";


            //    //modelBuilder.Entity<ApplicationUser>().HasData(

            //    // new ApplicationUser
            //    // {
            //    //     Id = user1Id,
            //    //     UserName = "user1",
            //    //     Email = "user1@example.com",
            //    //     CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            //    //     Password = "password1",
            //    // },
            //    //    new ApplicationUser
            //    //    {
            //    //        Id = user2Id,
            //    //        UserName = "user2",
            //    //        Email = "user2@example.com",
            //    //        CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            //    //        Password = "password1",
            //    //    }
            //    // );

            //    modelBuilder.Entity<Category>().HasData(
            //       new Category { CategoryId = electronicsCatId, Name = "Electronics", CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //       new Category { CategoryId = booksCatId, Name = "Books", CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //       new Category { CategoryId = clothingCatId, Name = "Clothing", CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //       new Category { CategoryId = furnitureCatId, Name = "Furniture", CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //       new Category { CategoryId = foodCatId, Name = "Food", CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) }
            //    );

            //    //Seed Products
            //    modelBuilder.Entity<Product>().HasData(
            //        new Product { ProductId = product1Id, Name = "Smartphone", Price = 499.99m, CategoryId = electronicsCatId, SupplierId = supplier1Id, CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new Product { ProductId = product2Id, Name = "Novel", Price = 19.99m, CategoryId = booksCatId, SupplierId = supplier2Id, CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new Product { ProductId = product3Id, Name = "T-shirt", Price = 15.99m, CategoryId = clothingCatId, SupplierId = supplier3Id, CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new Product { ProductId = product4Id, Name = "Sofa", Price = 799.99m, CategoryId = furnitureCatId, SupplierId = supplier4Id, CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new Product { ProductId = product5Id, Name = "Cereal", Price = 3.49m, CategoryId = foodCatId, SupplierId = supplier5Id, CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) }
            //    );

            //    // Seed Warehouses
            //    modelBuilder.Entity<Warehouse>().HasData(
            //        new Warehouse { WarehouseId = warehouse1Id, Name = "Lagos Warehouse", Location = "Ikeja" },
            //        new Warehouse { WarehouseId = warehouse2Id, Name = "Abuja Warehouse", Location = "Gwarinpa" },
            //        new Warehouse { WarehouseId = warehouse3Id, Name = "Port Harcourt Warehouse", Location = "D-Line" },
            //        new Warehouse { WarehouseId = warehouse4Id, Name = "Kano Warehouse", Location = "Kofar Ruwa" },
            //        new Warehouse { WarehouseId = warehouse5Id, Name = "Ogun Warehouse", Location = "Abeokuta" }
            //    );

            //    // Seed ProductWarehouse (junction table)
            //    modelBuilder.Entity<ProductWarehouse>().HasData(
            //        new ProductWarehouse { ProductWarehouseId = productWarehouse1Id, ProductId = product1Id, WarehouseId = warehouse1Id, Quantity = 150 },
            //        new ProductWarehouse { ProductWarehouseId = productWarehouse2Id, ProductId = product2Id, WarehouseId = warehouse2Id, Quantity = 80 },
            //        new ProductWarehouse { ProductWarehouseId = productWarehouse3Id, ProductId = product3Id, WarehouseId = warehouse3Id, Quantity = 200 },
            //        new ProductWarehouse { ProductWarehouseId = productWarehouse4Id, ProductId = product4Id, WarehouseId = warehouse4Id, Quantity = 30 },
            //        new ProductWarehouse { ProductWarehouseId = productWarehouse5Id, ProductId = product5Id, WarehouseId = warehouse5Id, Quantity = 500 }
            //    );

            //    // Seed InventoryMovements
            //    modelBuilder.Entity<InventoryMovement>().HasData(
            //        new InventoryMovement { InventoryMovementId = inventoryMovement1Id, ProductId = product1Id, WarehouseId = warehouse1Id, MovementType = MovementType.Inbound, QuantityChanged = 50, MovementDate = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new InventoryMovement { InventoryMovementId = inventoryMovement2Id, ProductId = product2Id, WarehouseId = warehouse4Id, MovementType = MovementType.Transfer, QuantityChanged = 30, MovementDate = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new InventoryMovement { InventoryMovementId = inventoryMovement3Id, ProductId = product5Id, WarehouseId = warehouse1Id, MovementType = MovementType.Outbound, QuantityChanged = 30, MovementDate = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new InventoryMovement { InventoryMovementId = inventoryMovement4Id, ProductId = product3Id, WarehouseId = warehouse5Id, MovementType = MovementType.Transfer, QuantityChanged = 30, MovementDate = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new InventoryMovement { InventoryMovementId = inventoryMovement5Id, ProductId = product2Id, WarehouseId = warehouse3Id, MovementType = MovementType.Outbound, QuantityChanged = 30, MovementDate = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) }
            //    );

            //    // Seed Suppliers
            //    modelBuilder.Entity<Supplier>().HasData(
            //        new Supplier { SupplierId = supplier1Id, Name = "Tech Supplies Co.", ContactEmail = "techsupplies@example.com", ContactInfo = "08012345678", Address = " 25, Okemagba Avenue, Agege, Lagos State" },
            //        new Supplier { SupplierId = supplier2Id, Name = "Book World", ContactEmail = "bookworld@example.com", ContactInfo = "08123456789", Address = " Ikeja City Mall, Abdulrazak Nwabude Drive" },
            //        new Supplier { SupplierId = supplier3Id, Name = "Fashion Hub", ContactEmail = "fashionhub@example.com", ContactInfo = "09012345678", Address = "10, Broad Street, Lagos Island" },
            //        new Supplier { SupplierId = supplier4Id, Name = "FurniStore", ContactEmail = "furnistore@example.com", ContactInfo = "08087654321", Address = "Km 19, Lekki-Epe Expressway, Lekki," },
            //        new Supplier { SupplierId = supplier5Id, Name = "Grocery Mart", ContactEmail = "grocerymart@example.com", ContactInfo = "08111223344", Address = "21, Ibadan Street, Surulere," }
            //    );

            //    // Seed AuditLogs
            //    modelBuilder.Entity<AuditLog>().HasData(
            //        new AuditLog { AuditLogId = auditLog1Id, Action = "Product Added", Details = "Product was added", PerformedBy = testUserId, CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new AuditLog { AuditLogId = auditLog2Id, Action = "Warehouse Created", Details = "Warehouse created", PerformedBy = testUserId, CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
            //        new AuditLog { AuditLogId = auditLog3Id, Action = "Inventory Movement", Details = "InventoryMovement recored", PerformedBy = testUserId, CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) }
            //    );

            base.OnModelCreating(modelBuilder);
        }

    }
}
