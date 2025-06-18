using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartDto>> GetAllCartsAsync()
        {
            var carts = await _unitOfWork.Repository<Cart>().GetAllAsync();
            return _mapper.Map<IEnumerable<CartDto>>(carts);
        }

        public async Task<CartDto?> GetCartByIdAsync(Guid id)
        {
            var cart = await _unitOfWork.Repository<Cart>().GetByIdAsync(id);
            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto?> GetCartWithItemsAsync(Guid id)
        {
            var cart = await _unitOfWork.Repository<Cart>()
                .GetSingleWithIncludeAsync(
                    c => c.Id == id,
                    c => c.CartItems,
                    c => c.User
                );
            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto?> GetCartByUserAsync(Guid userId)
        {
            var cart = await _unitOfWork.Repository<Cart>()
                .GetSingleWithIncludeAsync(
                    c => c.UserId == userId,
                    c => c.CartItems
                );
            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto?> GetCartBySessionAsync(string sessionId)
        {
            var cart = await _unitOfWork.Repository<Cart>()
                .GetSingleWithIncludeAsync(
                    c => c.SessionId == sessionId,
                    c => c.CartItems
                );
            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> CreateCartAsync(CreateCartDto createCartDto)
        {
            var cart = _mapper.Map<Cart>(createCartDto);
            await _unitOfWork.Repository<Cart>().AddAsync(cart);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> DeleteCartAsync(Guid id)
        {
            var cart = await _unitOfWork.Repository<Cart>().GetByIdAsync(id);
            if (cart == null)
                return false;

            _unitOfWork.Repository<Cart>().Remove(cart);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<CartItemDto> AddCartItemAsync(CreateCartItemDto createCartItemDto)
        {
            var cartItem = _mapper.Map<CartItem>(createCartItemDto);
            await _unitOfWork.Repository<CartItem>().AddAsync(cartItem);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CartItemDto>(cartItem);
        }

        public async Task<CartItemDto?> UpdateCartItemAsync(Guid id, UpdateCartItemDto updateCartItemDto)
        {
            var existingCartItem = await _unitOfWork.Repository<CartItem>().GetByIdAsync(id);
            if (existingCartItem == null)
                return null;

            _mapper.Map(updateCartItemDto, existingCartItem);
            _unitOfWork.Repository<CartItem>().Update(existingCartItem);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CartItemDto>(existingCartItem);
        }

        public async Task<bool> RemoveCartItemAsync(Guid id)
        {
            var cartItem = await _unitOfWork.Repository<CartItem>().GetByIdAsync(id);
            if (cartItem == null)
                return false;

            _unitOfWork.Repository<CartItem>().Remove(cartItem);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(Guid cartId)
        {
            var cartItems = await _unitOfWork.Repository<CartItem>()
                .FindAsync(ci => ci.CartId == cartId);
            
            if (!cartItems.Any())
                return false;

            _unitOfWork.Repository<CartItem>().RemoveRange(cartItems);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
