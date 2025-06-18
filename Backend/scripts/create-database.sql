-- Create Flora Database
USE master;
GO

-- Drop database if exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'FloraDB')
BEGIN
    ALTER DATABASE FloraDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE FloraDB;
END
GO

-- Create new database
CREATE DATABASE FloraDB;
GO

USE FloraDB;
GO

-- Create Users table
CREATE TABLE Users (
    UserId int IDENTITY(1,1) PRIMARY KEY,
    FullName nvarchar(100) NOT NULL,
    Email nvarchar(100) NOT NULL UNIQUE,
    PasswordHash nvarchar(255) NOT NULL,
    PhoneNumber nvarchar(15) NULL,
    Address nvarchar(500) NULL,
    Role nvarchar(50) NOT NULL DEFAULT 'Customer',
    IsActive bit NOT NULL DEFAULT 1,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt datetime2 NULL
);

-- Create Categories table
CREATE TABLE Categories (
    CategoryId int IDENTITY(1,1) PRIMARY KEY,
    CategoryName nvarchar(100) NOT NULL,
    Description nvarchar(500) NULL,
    ImageUrl nvarchar(255) NULL,
    IsActive bit NOT NULL DEFAULT 1,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt datetime2 NULL
);

-- Create Products table
CREATE TABLE Products (
    ProductId int IDENTITY(1,1) PRIMARY KEY,
    ProductName nvarchar(200) NOT NULL,
    Description nvarchar(1000) NULL,
    Price decimal(18,2) NOT NULL,
    DiscountPrice decimal(18,2) NULL,
    StockQuantity int NOT NULL DEFAULT 0,
    ImageUrl nvarchar(255) NULL,
    ImageUrls nvarchar(1000) NULL,
    IsActive bit NOT NULL DEFAULT 1,
    IsFeatured bit NOT NULL DEFAULT 0,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt datetime2 NULL,
    CategoryId int NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

-- Create Carts table
CREATE TABLE Carts (
    CartId int IDENTITY(1,1) PRIMARY KEY,
    SessionId nvarchar(100) NULL,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt datetime2 NULL,
    UserId int NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE SET NULL
);

-- Create Orders table
CREATE TABLE Orders (
    OrderId int IDENTITY(1,1) PRIMARY KEY,
    OrderNumber nvarchar(50) NOT NULL UNIQUE,
    TotalAmount decimal(18,2) NOT NULL,
    DiscountAmount decimal(18,2) NULL,
    ShippingFee decimal(18,2) NULL,
    FinalAmount decimal(18,2) NOT NULL,
    Status nvarchar(50) NOT NULL DEFAULT 'Pending',
    CustomerName nvarchar(100) NOT NULL,
    CustomerEmail nvarchar(100) NOT NULL,
    CustomerPhone nvarchar(15) NULL,
    ShippingAddress nvarchar(500) NOT NULL,
    Notes nvarchar(1000) NULL,
    OrderDate datetime2 NOT NULL DEFAULT GETUTCDATE(),
    ShippedDate datetime2 NULL,
    DeliveredDate datetime2 NULL,
    UserId int NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE SET NULL
);

-- Create OrderDetails table
CREATE TABLE OrderDetails (
    OrderDetailId int IDENTITY(1,1) PRIMARY KEY,
    Quantity int NOT NULL,
    UnitPrice decimal(18,2) NOT NULL,
    DiscountPrice decimal(18,2) NULL,
    TotalPrice decimal(18,2) NOT NULL,
    OrderId int NOT NULL,
    ProductId int NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- Create CartItems table
CREATE TABLE CartItems (
    CartItemId int IDENTITY(1,1) PRIMARY KEY,
    Quantity int NOT NULL,
    UnitPrice decimal(18,2) NOT NULL,
    AddedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    CartId int NOT NULL,
    ProductId int NOT NULL,
    FOREIGN KEY (CartId) REFERENCES Carts(CartId) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- Create indexes for better performance
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Orders_OrderNumber ON Orders(OrderNumber);
CREATE INDEX IX_Products_ProductName ON Products(ProductName);
CREATE INDEX IX_Categories_CategoryName ON Categories(CategoryName);
CREATE INDEX IX_Products_CategoryId ON Products(CategoryId);
CREATE INDEX IX_OrderDetails_OrderId ON OrderDetails(OrderId);
CREATE INDEX IX_OrderDetails_ProductId ON OrderDetails(ProductId);
CREATE INDEX IX_CartItems_CartId ON CartItems(CartId);
CREATE INDEX IX_CartItems_ProductId ON CartItems(ProductId);

PRINT 'Database FloraDB created successfully!';
