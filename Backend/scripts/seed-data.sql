-- Seed initial data for Flora Database
USE FloraDB;
GO

-- Insert Categories
INSERT INTO Categories (CategoryName, Description, IsActive, CreatedAt) VALUES
('Hoa Tươi', 'Các loại hoa tươi đẹp cho mọi dịp', 1, GETUTCDATE()),
('Hoa Cưới', 'Hoa cưới và trang trí tiệc cưới', 1, GETUTCDATE()),
('Hoa Sinh Nhật', 'Hoa tặng sinh nhật ý nghĩa', 1, GETUTCDATE()),
('Hoa Khai Trương', 'Hoa chúc mừng khai trương', 1, GETUTCDATE()),
('Hoa Chia Buồn', 'Hoa chia buồn trang trọng', 1, GETUTCDATE()),
('Hoa Valentine', 'Hoa tặng người yêu ngày Valentine', 1, GETUTCDATE());

-- Insert Admin User (password should be hashed in real application)
INSERT INTO Users (FullName, Email, PasswordHash, Role, IsActive, CreatedAt) VALUES
('Administrator', 'admin@flora.com', '$2a$11$rQZrHzXgzXzXzXzXzXzXzXzXzXzXzXzXzXzXzXzXzXzXzXzXzXzXzX', 'Admin', 1, GETUTCDATE());

-- Insert Sample Products
INSERT INTO Products (ProductName, Description, Price, DiscountPrice, StockQuantity, CategoryId, IsActive, IsFeatured, CreatedAt) VALUES
-- Hoa Tươi
('Bó Hoa Hồng Đỏ', 'Bó hoa hồng đỏ tươi đẹp, thể hiện tình yêu chân thành', 250000, NULL, 50, 1, 1, 1, GETUTCDATE()),
('Bó Hoa Hồng Trắng', 'Bó hoa hồng trắng tinh khôi, thanh tao', 230000, NULL, 45, 1, 1, 0, GETUTCDATE()),
('Bó Hoa Tulip', 'Bó hoa tulip đầy màu sắc, tươi mới', 180000, 150000, 30, 1, 1, 1, GETUTCDATE()),
('Bó Hoa Ly', 'Bó hoa ly thơm ngát, sang trọng', 200000, NULL, 25, 1, 1, 0, GETUTCDATE()),

-- Hoa Cưới
('Hoa Cưới Cầm Tay', 'Hoa cưới cầm tay tinh tế cho cô dâu', 500000, NULL, 20, 2, 1, 1, GETUTCDATE()),
('Hoa Cài Áo Chú Rể', 'Hoa cài áo thanh lịch cho chú rể', 50000, NULL, 50, 2, 1, 0, GETUTCDATE()),
('Trang Trí Bàn Tiệc Cưới', 'Hoa trang trí bàn tiệc cưới lãng mạn', 300000, NULL, 15, 2, 1, 1, GETUTCDATE()),

-- Hoa Sinh Nhật
('Bó Hoa Sinh Nhật Rực Rỡ', 'Bó hoa sinh nhật đầy màu sắc và ý nghĩa', 180000, NULL, 30, 3, 1, 1, GETUTCDATE()),
('Giỏ Hoa Sinh Nhật', 'Giỏ hoa sinh nhật xinh xắn, đáng yêu', 220000, 200000, 20, 3, 1, 0, GETUTCDATE()),
('Hoa Hướng Dương Sinh Nhật', 'Hoa hướng dương tươi vui cho sinh nhật', 160000, NULL, 35, 3, 1, 0, GETUTCDATE()),

-- Hoa Khai Trương
('Lẵng Hoa Khai Trương Sang Trọng', 'Lẵng hoa khai trương sang trọng, mang lại may mắn', 800000, NULL, 15, 4, 1, 1, GETUTCDATE()),
('Chậu Hoa Khai Trương', 'Chậu hoa khai trương tươi lâu', 350000, NULL, 25, 4, 1, 0, GETUTCDATE()),

-- Hoa Chia Buồn
('Vòng Hoa Chia Buồn', 'Vòng hoa chia buồn trang trọng', 600000, NULL, 10, 5, 1, 0, GETUTCDATE()),
('Lẵng Hoa Chia Buồn', 'Lẵng hoa chia buồn thanh tịnh', 450000, NULL, 12, 5, 1, 0, GETUTCDATE()),

-- Hoa Valentine
('Bó Hoa Valentine Đặc Biệt', 'Bó hoa Valentine đặc biệt cho người yêu', 350000, 300000, 40, 6, 1, 1, GETUTCDATE()),
('Hộp Hoa Valentine', 'Hộp hoa Valentine sang trọng', 280000, NULL, 30, 6, 1, 1, GETUTCDATE());

-- Create sample order
DECLARE @OrderNumber NVARCHAR(50) = 'ORD' + FORMAT(GETDATE(), 'yyyyMMddHHmmss');

INSERT INTO Orders (OrderNumber, TotalAmount, FinalAmount, Status, CustomerName, CustomerEmail, CustomerPhone, ShippingAddress, OrderDate) VALUES
(@OrderNumber, 430000, 430000, 'Delivered', N'Nguyễn Văn A', 'customer@example.com', '0123456789', N'123 Đường ABC, Quận 1, TP.HCM', DATEADD(day, -5, GETUTCDATE()));

DECLARE @OrderId INT = SCOPE_IDENTITY();

-- Insert order details
INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, TotalPrice) VALUES
(@OrderId, 1, 1, 250000, 250000),
(@OrderId, 3, 1, 180000, 180000);

PRINT 'Sample data inserted successfully!';
