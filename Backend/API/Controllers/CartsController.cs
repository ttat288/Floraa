using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Get all carts
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCarts()
        {
            var carts = await _cartService.GetAllCartsAsync();
            return Ok(carts);
        }

        /// <summary>
        /// Get cart by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCart(Guid id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            
            if (cart == null)
            {
                return NotFound($"Cart with ID {id} not found.");
            }

            return Ok(cart);
        }

        /// <summary>
        /// Get cart with items by ID
        /// </summary>
        [HttpGet("{id}/items")]
        public async Task<ActionResult<CartDto>> GetCartWithItems(Guid id)
        {
            var cart = await _cartService.GetCartWithItemsAsync(id);
            
            if (cart == null)
            {
                return NotFound($"Cart with ID {id} not found.");
            }

            return Ok(cart);
        }

        /// <summary>
        /// Get cart by user ID
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<CartDto>> GetCartByUser(Guid userId)
        {
            var cart = await _cartService.GetCartByUserAsync(userId);
            
            if (cart == null)
            {
                return NotFound($"Cart for user {userId} not found.");
            }

            return Ok(cart);
        }

        /// <summary>
        /// Get cart by session ID
        /// </summary>
        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<CartDto>> GetCartBySession(string sessionId)
        {
            var cart = await _cartService.GetCartBySessionAsync(sessionId);
            
            if (cart == null)
            {
                return NotFound($"Cart for session {sessionId} not found.");
            }

            return Ok(cart);
        }

        /// <summary>
        /// Create a new cart
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateCart(CreateCartDto createCartDto)
        {
            var cart = await _cartService.CreateCartAsync(createCartDto);
            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        /// <summary>
        /// Delete a cart
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var result = await _cartService.DeleteCartAsync(id);
            
            if (!result)
            {
                return NotFound($"Cart with ID {id} not found.");
            }

            return NoContent();
        }

        /// <summary>
        /// Add item to cart
        /// </summary>
        [HttpPost("items")]
        public async Task<ActionResult<CartItemDto>> AddCartItem(CreateCartItemDto createCartItemDto)
        {
            var cartItem = await _cartService.AddCartItemAsync(createCartItemDto);
            return Ok(cartItem);
        }

        /// <summary>
        /// Update cart item
        /// </summary>
        [HttpPut("items/{id}")]
        public async Task<ActionResult<CartItemDto>> UpdateCartItem(Guid id, UpdateCartItemDto updateCartItemDto)
        {
            var cartItem = await _cartService.UpdateCartItemAsync(id, updateCartItemDto);
            
            if (cartItem == null)
            {
                return NotFound($"Cart item with ID {id} not found.");
            }

            return Ok(cartItem);
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        [HttpDelete("items/{id}")]
        public async Task<IActionResult> RemoveCartItem(Guid id)
        {
            var result = await _cartService.RemoveCartItemAsync(id);
            
            if (!result)
            {
                return NotFound($"Cart item with ID {id} not found.");
            }

            return NoContent();
        }

        /// <summary>
        /// Clear all items from cart
        /// </summary>
        [HttpDelete("{cartId}/clear")]
        public async Task<IActionResult> ClearCart(Guid cartId)
        {
            var result = await _cartService.ClearCartAsync(cartId);
            
            if (!result)
            {
                return NotFound($"Cart with ID {cartId} not found or already empty.");
            }

            return NoContent();
        }
    }
}
