namespace Service.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal FinalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId { get; set; }
        public UserDto? User { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateOrderDto
    {
        public decimal TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal FinalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public Guid? UserId { get; set; }
        public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
    }

    public class CreateOrderDetailDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid ProductId { get; set; }
    }

    public class UpdateOrderDto
    {
        public string Status { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}
