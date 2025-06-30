namespace Application.DTOs.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public Guid ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public Guid? ThemeId { get; set; }
    public string? ThemeName { get; set; }
    public Guid? OccasionId { get; set; }
    public string? OccasionName { get; set; }
    public Guid? ColorId { get; set; }
    public string? ColorName { get; set; }
    public Guid? SubProductId { get; set; }
    public string? SubProductMeaning { get; set; }
    public string? Composition { get; set; }
    public int SoldCount { get; set; }
    public DateTime CreatedAt { get; set; }

    // Legacy fields for backward compatibility
    public Guid CategoryId => ShopId;
    public string CategoryName => ShopName;
}
