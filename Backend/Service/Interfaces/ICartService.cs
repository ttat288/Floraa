using Service.DTOs;

namespace Service.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDto>> GetAllCartsAsync();
        Task<CartDto?> GetCartByIdAsync(Guid id);
        Task<CartDto?> GetCartWithItemsAsync(Guid id);
        Task<CartDto?> GetCartByUserAsync(Guid userId);
        Task<CartDto?> GetCartBySessionAsync(string sessionId);
        Task<CartDto> CreateCartAsync(CreateCartDto createCartDto);
        Task<bool> DeleteCartAsync(Guid id);
        Task<CartItemDto> AddCartItemAsync(CreateCartItemDto createCartItemDto);
        Task<CartItemDto?> UpdateCartItemAsync(Guid id, UpdateCartItemDto updateCartItemDto);
        Task<bool> RemoveCartItemAsync(Guid id);
        Task<bool> ClearCartAsync(Guid cartId);
    }
}
