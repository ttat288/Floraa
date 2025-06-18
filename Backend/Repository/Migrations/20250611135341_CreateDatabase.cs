using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmationToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PasswordResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageUrls = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShippingFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedAt", "Description", "ImageUrl", "IsActive", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Hoa Tươi", new DateTime(2025, 6, 11, 13, 53, 40, 369, DateTimeKind.Utc).AddTicks(9650), "Các loại hoa tươi đẹp cho mọi dịp", null, true, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Hoa Cưới", new DateTime(2025, 6, 11, 13, 53, 40, 369, DateTimeKind.Utc).AddTicks(9653), "Hoa cưới và trang trí tiệc cưới", null, true, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Hoa Sinh Nhật", new DateTime(2025, 6, 11, 13, 53, 40, 369, DateTimeKind.Utc).AddTicks(9656), "Hoa tặng sinh nhật ý nghĩa", null, true, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Hoa Khai Trương", new DateTime(2025, 6, 11, 13, 53, 40, 369, DateTimeKind.Utc).AddTicks(9658), "Hoa chúc mừng khai trương", null, true, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Hoa Chia Buồn", new DateTime(2025, 6, 11, 13, 53, 40, 369, DateTimeKind.Utc).AddTicks(9661), "Hoa chia buồn trang trọng", null, true, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Hoa Valentine", new DateTime(2025, 6, 11, 13, 53, 40, 369, DateTimeKind.Utc).AddTicks(9663), "Hoa tặng người yêu ngày Valentine", null, true, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "EmailConfirmationToken", "EmailConfirmed", "FullName", "IsActive", "LastLoginAt", "PasswordHash", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 6, 11, 13, 53, 40, 499, DateTimeKind.Utc).AddTicks(9638), "admin@flora.com", null, true, "Administrator", true, null, "$2a$11$95bETPY9DW7/6BQJBzRKvetEdAkEc/4sbMSuB/K5wN5OWAARc3cHa", null, null, null, "Admin", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1298), "staff@flora.com", null, true, "Staff User", true, null, "$2a$11$PMvV1Fl/MR/9cc99uDfLjeoJhpvEGEdeyUcaj9DJ2jyXuo3nOpMu.", null, null, null, "Staff", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "DiscountPrice", "ImageUrl", "ImageUrls", "IsActive", "IsFeatured", "Price", "ProductName", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1864), "Bó hoa hồng đỏ tươi đẹp, thể hiện tình yêu chân thành", null, null, null, true, true, 250000m, "Bó Hoa Hồng Đỏ", 50, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1869), "Bó hoa hồng trắng tinh khôi, thanh tao", null, null, null, true, false, 230000m, "Bó Hoa Hồng Trắng", 45, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1877), "Bó hoa tulip đầy màu sắc, tươi mới", 150000m, null, null, true, true, 180000m, "Bó Hoa Tulip", 30, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1893), "Bó hoa ly thơm ngát, sang trọng", null, null, null, true, false, 200000m, "Bó Hoa Ly", 25, null },
                    { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1915), "Hoa cưới cầm tay tinh tế cho cô dâu", null, null, null, true, true, 500000m, "Hoa Cưới Cầm Tay", 20, null },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1929), "Hoa cài áo thanh lịch cho chú rể", null, null, null, true, false, 50000m, "Hoa Cài Áo Chú Rể", 50, null },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1933), "Hoa trang trí bàn tiệc cưới lãng mạn", null, null, null, true, true, 300000m, "Trang Trí Bàn Tiệc Cưới", 15, null },
                    { new Guid("30000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1947), "Bó hoa sinh nhật đầy màu sắc và ý nghĩa", null, null, null, true, true, 180000m, "Bó Hoa Sinh Nhật Rực Rỡ", 30, null },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1951), "Giỏ hoa sinh nhật xinh xắn, đáng yêu", 200000m, null, null, true, false, 220000m, "Giỏ Hoa Sinh Nhật", 20, null },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(1954), "Hoa hướng dương tươi vui cho sinh nhật", null, null, null, true, false, 160000m, "Hoa Hướng Dương Sinh Nhật", 35, null },
                    { new Guid("40000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(2069), "Lẵng hoa khai trương sang trọng, mang lại may mắn", null, null, null, true, true, 800000m, "Lẵng Hoa Khai Trương Sang Trọng", 15, null },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(2073), "Chậu hoa khai trương tươi lâu", null, null, null, true, false, 350000m, "Chậu Hoa Khai Trương", 25, null },
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(2081), "Vòng hoa chia buồn trang trọng", null, null, null, true, false, 600000m, "Vòng Hoa Chia Buồn", 10, null },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(2092), "Lẵng hoa chia buồn thanh tịnh", null, null, null, true, false, 450000m, "Lẵng Hoa Chia Buồn", 12, null },
                    { new Guid("60000000-0000-0000-0000-000000000001"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(2095), "Bó hoa Valentine đặc biệt cho người yêu", 300000m, null, null, true, true, 350000m, "Bó Hoa Valentine Đặc Biệt", 40, null },
                    { new Guid("60000000-0000-0000-0000-000000000002"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 6, 11, 13, 53, 40, 641, DateTimeKind.Utc).AddTicks(2101), "Hộp hoa Valentine sang trọng", null, null, null, true, true, 280000m, "Hộp Hoa Valentine", 30, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
