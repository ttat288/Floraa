using Service.DTOs;

namespace Service.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
        Task<OrderDto?> GetOrderWithDetailsAsync(Guid id);
        Task<OrderDto?> GetOrderByNumberAsync(string orderNumber);
        Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(Guid userId);
        Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderDto?> UpdateOrderAsync(Guid id, UpdateOrderDto updateOrderDto);
        Task<bool> DeleteOrderAsync(Guid id);
    }
}
