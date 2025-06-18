namespace Service.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrls { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
    }

    public class CreateProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrls { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public Guid CategoryId { get; set; }
    }

    public class UpdateProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrls { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public Guid CategoryId { get; set; }
    }
}
