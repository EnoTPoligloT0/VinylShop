CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Users" (
    "UserId" uuid NOT NULL,
    "FirstName" character varying(100) NOT NULL,
    "LastName" character varying(100) NOT NULL,
    "PasswordHash" text NOT NULL,
    "Email" character varying(100) NOT NULL,
    "PhoneNumber" character varying(15),
    "AddressLine1" character varying(200),
    "AddressLine2" character varying(200),
    "City" character varying(100),
    "State" character varying(100),
    "ZipCode" character varying(20),
    CONSTRAINT "PK_Users" PRIMARY KEY ("UserId")
);

CREATE TABLE "Vinyls" (
    "Id" uuid NOT NULL,
    "Title" character varying(100) NOT NULL,
    "Artist" character varying(100) NOT NULL,
    "Genre" character varying(50) NOT NULL,
    "ReleaseYear" integer NOT NULL,
    "Price" numeric(18,2) NOT NULL,
    "Stock" integer NOT NULL,
    "Description" character varying(500) NOT NULL,
    "IsAvailable" boolean NOT NULL,
    CONSTRAINT "PK_Vinyls" PRIMARY KEY ("Id")
);

CREATE TABLE "Orders" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "OrderDate" timestamp with time zone NOT NULL,
    "TotalAmount" numeric(18,2) NOT NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Orders_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId") ON DELETE CASCADE
);

CREATE TABLE "OrderItems" (
    "Id" uuid NOT NULL,
    "OrderId" uuid NOT NULL,
    "VinylId" uuid NOT NULL,
    "Quantity" integer NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL,
    CONSTRAINT "PK_OrderItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_OrderItems_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_OrderItems_Vinyls_VinylId" FOREIGN KEY ("VinylId") REFERENCES "Vinyls" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Payments" (
    "PaymentId" uuid NOT NULL,
    "OrderId" uuid NOT NULL,
    "PaymentDate" timestamp with time zone NOT NULL,
    "Amount" numeric(18,2) NOT NULL,
    "PaymentMethod" character varying(50) NOT NULL,
    CONSTRAINT "PK_Payments" PRIMARY KEY ("PaymentId"),
    CONSTRAINT "FK_Payments_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Shipments" (
    "ShipmentId" uuid NOT NULL,
    "OrderId" uuid NOT NULL,
    "ShipmentDate" timestamp with time zone NOT NULL,
    "TrackingNumber" character varying(100) NOT NULL,
    "ShipmentStatus" character varying(50) NOT NULL,
    CONSTRAINT "PK_Shipments" PRIMARY KEY ("ShipmentId"),
    CONSTRAINT "FK_Shipments_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_OrderItems_OrderId" ON "OrderItems" ("OrderId");

CREATE INDEX "IX_OrderItems_VinylId" ON "OrderItems" ("VinylId");

CREATE INDEX "IX_Orders_UserId" ON "Orders" ("UserId");

CREATE INDEX "IX_Payments_OrderId" ON "Payments" ("OrderId");

CREATE INDEX "IX_Shipments_OrderId" ON "Shipments" ("OrderId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240731230321_initial', '8.0.0');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240731233558_SecondMigration', '8.0.0');

COMMIT;

