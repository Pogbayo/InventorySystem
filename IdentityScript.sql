IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE TABLE [AuditLogs] (
        [AuditLogId] uniqueidentifier NOT NULL,
        [PerformedBy] uniqueidentifier NOT NULL,
        [Action] nvarchar(max) NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([AuditLogId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE TABLE [Categories] (
        [CategoryId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE TABLE [Suppliers] (
        [SupplierId] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [ContactInfo] nvarchar(50) NOT NULL,
        [ContactEmail] nvarchar(100) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Suppliers] PRIMARY KEY ([SupplierId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE TABLE [Warehouses] (
        [WarehouseId] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Location] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Warehouses] PRIMARY KEY ([WarehouseId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE TABLE [Products] (
        [ProductId] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [CategoryId] uniqueidentifier NOT NULL,
        [SupplierId] uniqueidentifier NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId]),
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Products_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([SupplierId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE TABLE [InventoryMovements] (
        [InventoryMovementId] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [WarehouseId] uniqueidentifier NOT NULL,
        [MovementType] nvarchar(max) NOT NULL,
        [QuantityChanged] int NOT NULL,
        [MovementDate] datetime2 NOT NULL,
        CONSTRAINT [PK_InventoryMovements] PRIMARY KEY ([InventoryMovementId]),
        CONSTRAINT [FK_InventoryMovements_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE,
        CONSTRAINT [FK_InventoryMovements_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([WarehouseId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE TABLE [ProductWarehouses] (
        [ProductWarehouseId] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [WarehouseId] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        CONSTRAINT [PK_ProductWarehouses] PRIMARY KEY ([ProductWarehouseId]),
        CONSTRAINT [FK_ProductWarehouses_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductWarehouses_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([WarehouseId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CreatedAt', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] ON;
    EXEC(N'INSERT INTO [Categories] ([CategoryId], [CreatedAt], [Name])
    VALUES (''aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'', ''2023-01-01T00:00:00.0000000Z'', N''Electronics''),
    (''bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb'', ''2023-01-01T00:00:00.0000000Z'', N''Books''),
    (''cccccccc-cccc-cccc-cccc-cccccccccccc'', ''2023-01-01T00:00:00.0000000Z'', N''Clothing''),
    (''dddddddd-dddd-dddd-dddd-dddddddddddd'', ''2023-01-01T00:00:00.0000000Z'', N''Furniture''),
    (''eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee'', ''2023-01-01T00:00:00.0000000Z'', N''Food'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CreatedAt', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_InventoryMovements_ProductId] ON [InventoryMovements] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_InventoryMovements_WarehouseId] ON [InventoryMovements] ([WarehouseId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Products_SupplierId] ON [Products] ([SupplierId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ProductWarehouses_ProductId] ON [ProductWarehouses] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ProductWarehouses_WarehouseId] ON [ProductWarehouses] ([WarehouseId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422112801_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250422112801_InitialCreate', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422113400_SeedingSupplierEntity'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SupplierId', N'Address', N'ContactEmail', N'ContactInfo', N'Name') AND [object_id] = OBJECT_ID(N'[Suppliers]'))
        SET IDENTITY_INSERT [Suppliers] ON;
    EXEC(N'INSERT INTO [Suppliers] ([SupplierId], [Address], [ContactEmail], [ContactInfo], [Name])
    VALUES (''1111aaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'', N'' 25, Okemagba Avenue, Agege, Lagos State'', N''techsupplies@example.com'', N''08012345678'', N''Tech Supplies Co.''),
    (''2222bbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb'', N'' Ikeja City Mall, Abdulrazak Nwabude Drive'', N''bookworld@example.com'', N''08123456789'', N''Book World''),
    (''3333cccc-cccc-cccc-cccc-cccccccccccc'', N''10, Broad Street, Lagos Island'', N''fashionhub@example.com'', N''09012345678'', N''Fashion Hub''),
    (''4444dddd-dddd-dddd-dddd-dddddddddddd'', N''Km 19, Lekki-Epe Expressway, Lekki,'', N''furnistore@example.com'', N''08087654321'', N''FurniStore''),
    (''5555eeee-eeee-eeee-eeee-eeeeeeeeeeee'', N''21, Ibadan Street, Surulere,'', N''grocerymart@example.com'', N''08111223344'', N''Grocery Mart'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SupplierId', N'Address', N'ContactEmail', N'ContactInfo', N'Name') AND [object_id] = OBJECT_ID(N'[Suppliers]'))
        SET IDENTITY_INSERT [Suppliers] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422113400_SeedingSupplierEntity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250422113400_SeedingSupplierEntity', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422113542_SeedingProducts'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AuditLogId', N'Action', N'CreatedAt', N'Details', N'PerformedBy') AND [object_id] = OBJECT_ID(N'[AuditLogs]'))
        SET IDENTITY_INSERT [AuditLogs] ON;
    EXEC(N'INSERT INTO [AuditLogs] ([AuditLogId], [Action], [CreatedAt], [Details], [PerformedBy])
    VALUES (''abcabc01-0000-0000-0000-000000000001'', N''Product Added'', ''2023-01-01T00:00:00.0000000Z'', N''Product was added'', ''11111111-1111-1111-1111-111111111111''),
    (''abcabc02-0000-0000-0000-000000000002'', N''Warehouse Created'', ''2023-01-01T00:00:00.0000000Z'', N''Warehouse created'', ''11111111-1111-1111-1111-111111111111''),
    (''abcabc03-0000-0000-0000-000000000003'', N''Inventory Movement'', ''2023-01-01T00:00:00.0000000Z'', N''InventoryMovement recored'', ''11111111-1111-1111-1111-111111111111'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AuditLogId', N'Action', N'CreatedAt', N'Details', N'PerformedBy') AND [object_id] = OBJECT_ID(N'[AuditLogs]'))
        SET IDENTITY_INSERT [AuditLogs] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422113542_SeedingProducts'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'CategoryId', N'CreatedAt', N'Name', N'Price', N'SupplierId') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] ON;
    EXEC(N'INSERT INTO [Products] ([ProductId], [CategoryId], [CreatedAt], [Name], [Price], [SupplierId])
    VALUES (''11111111-aaaa-aaaa-aaaa-111111111111'', ''aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'', ''2023-01-01T00:00:00.0000000Z'', N''Smartphone'', 499.99, ''1111aaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa''),
    (''22222222-bbbb-bbbb-bbbb-222222222222'', ''bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb'', ''2023-01-01T00:00:00.0000000Z'', N''Novel'', 19.99, ''2222bbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb''),
    (''33333333-cccc-cccc-cccc-333333333333'', ''cccccccc-cccc-cccc-cccc-cccccccccccc'', ''2023-01-01T00:00:00.0000000Z'', N''T-shirt'', 15.99, ''3333cccc-cccc-cccc-cccc-cccccccccccc''),
    (''44444444-dddd-dddd-dddd-444444444444'', ''dddddddd-dddd-dddd-dddd-dddddddddddd'', ''2023-01-01T00:00:00.0000000Z'', N''Sofa'', 799.99, ''4444dddd-dddd-dddd-dddd-dddddddddddd''),
    (''55555555-eeee-eeee-eeee-555555555555'', ''eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee'', ''2023-01-01T00:00:00.0000000Z'', N''Cereal'', 3.49, ''5555eeee-eeee-eeee-eeee-eeeeeeeeeeee'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'CategoryId', N'CreatedAt', N'Name', N'Price', N'SupplierId') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422113542_SeedingProducts'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250422113542_SeedingProducts', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422113733_SeedingWarehouses'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WarehouseId', N'Location', N'Name') AND [object_id] = OBJECT_ID(N'[Warehouses]'))
        SET IDENTITY_INSERT [Warehouses] ON;
    EXEC(N'INSERT INTO [Warehouses] ([WarehouseId], [Location], [Name])
    VALUES (''aaaa1111-1111-1111-1111-111111111111'', N''Ikeja'', N''Lagos Warehouse''),
    (''bbbb2222-2222-2222-2222-222222222222'', N''Gwarinpa'', N''Abuja Warehouse''),
    (''cccc3333-3333-3333-3333-333333333333'', N''D-Line'', N''Port Harcourt Warehouse''),
    (''dddd4444-4444-4444-4444-444444444444'', N''Kofar Ruwa'', N''Kano Warehouse''),
    (''eeee5555-5555-5555-5555-555555555555'', N''Abeokuta'', N''Ogun Warehouse'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'WarehouseId', N'Location', N'Name') AND [object_id] = OBJECT_ID(N'[Warehouses]'))
        SET IDENTITY_INSERT [Warehouses] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422113733_SeedingWarehouses'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250422113733_SeedingWarehouses', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422114029_SeedingProductWarehouse'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InventoryMovementId', N'MovementDate', N'MovementType', N'ProductId', N'QuantityChanged', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[InventoryMovements]'))
        SET IDENTITY_INSERT [InventoryMovements] ON;
    EXEC(N'INSERT INTO [InventoryMovements] ([InventoryMovementId], [MovementDate], [MovementType], [ProductId], [QuantityChanged], [WarehouseId])
    VALUES (''a1a1a1a1-0000-0000-0000-000000000001'', ''2023-01-01T00:00:00.0000000Z'', N''Inbound'', ''11111111-aaaa-aaaa-aaaa-111111111111'', 50, ''aaaa1111-1111-1111-1111-111111111111''),
    (''b2b2b2b2-0000-0000-0000-000000000002'', ''2023-01-01T00:00:00.0000000Z'', N''Transfer'', ''22222222-bbbb-bbbb-bbbb-222222222222'', 30, ''dddd4444-4444-4444-4444-444444444444''),
    (''c3c3c3c3-0000-0000-0000-000000000003'', ''2023-01-01T00:00:00.0000000Z'', N''Outbound'', ''55555555-eeee-eeee-eeee-555555555555'', 30, ''aaaa1111-1111-1111-1111-111111111111''),
    (''d4d4d4d4-0000-0000-0000-000000000004'', ''2023-01-01T00:00:00.0000000Z'', N''Transfer'', ''33333333-cccc-cccc-cccc-333333333333'', 30, ''eeee5555-5555-5555-5555-555555555555''),
    (''e5e5e5e5-0000-0000-0000-000000000005'', ''2023-01-01T00:00:00.0000000Z'', N''Outbound'', ''22222222-bbbb-bbbb-bbbb-222222222222'', 30, ''cccc3333-3333-3333-3333-333333333333'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'InventoryMovementId', N'MovementDate', N'MovementType', N'ProductId', N'QuantityChanged', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[InventoryMovements]'))
        SET IDENTITY_INSERT [InventoryMovements] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422114029_SeedingProductWarehouse'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductWarehouseId', N'ProductId', N'Quantity', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ProductWarehouses]'))
        SET IDENTITY_INSERT [ProductWarehouses] ON;
    EXEC(N'INSERT INTO [ProductWarehouses] ([ProductWarehouseId], [ProductId], [Quantity], [WarehouseId])
    VALUES (''0021f1f1-0000-0000-0000-000000000001'', ''33333333-cccc-cccc-cccc-333333333333'', 200, ''cccc3333-3333-3333-3333-333333333333''),
    (''23f1f1f1-0020-0000-0000-000000000001'', ''44444444-dddd-dddd-dddd-444444444444'', 30, ''dddd4444-4444-4444-4444-444444444444''),
    (''f1f1f1f1-0000-0000-0000-000000000001'', ''11111111-aaaa-aaaa-aaaa-111111111111'', 150, ''aaaa1111-1111-1111-1111-111111111111''),
    (''f9dc1f1f-0000-0000-0000-000000000001'', ''22222222-bbbb-bbbb-bbbb-222222222222'', 80, ''bbbb2222-2222-2222-2222-222222222222''),
    (''fc1b1f1f-0000-0000-0000-000000000001'', ''55555555-eeee-eeee-eeee-555555555555'', 500, ''eeee5555-5555-5555-5555-555555555555'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductWarehouseId', N'ProductId', N'Quantity', N'WarehouseId') AND [object_id] = OBJECT_ID(N'[ProductWarehouses]'))
        SET IDENTITY_INSERT [ProductWarehouses] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250422114029_SeedingProductWarehouse'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250422114029_SeedingProductWarehouse', N'9.0.4');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250430120605_AddIdentityTables'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250430120605_AddIdentityTables', N'9.0.4');
END;

COMMIT;
GO

