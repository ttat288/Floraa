namespace Service.DTOs
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public string? SessionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UserId { get; set; }
        public UserDto? User { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new();
    }

    public class CartItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime AddedAt { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCartDto
    {
        public string? SessionId { get; set; }
        public Guid? UserId { get; set; }
    }

    public class CreateCartItemDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
    }

    public class UpdateCartItemDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
