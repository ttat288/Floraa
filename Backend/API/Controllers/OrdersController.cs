using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using API.Extensions;
using API.Attributes;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all orders - Admin/Staff only
        /// </summary>
        [HttpGet]
        [AuthorizeRole("Admin", "Staff")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Get current user's orders - Tự động lấy userId từ token
        /// </summary>
        [HttpGet("my-orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetMyOrders()
        {
            try
            {
                var userId = User.GetUserId();
                var orders = await _orderService.GetOrdersByUserAsync(userId);
                return Ok(orders);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get order by ID - Admin/Staff or order owner
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            try
            {
                var order = await _orderService.GetOrderWithDetailsAsync(id);

                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                var currentUserId = User.GetUserId();
                var currentUserRole = User.GetUserRole();

                // Allow if admin/staff or order owner
                if (currentUserRole != "Admin" && currentUserRole != "Staff" && order.UserId != currentUserId)
                {
                    return Forbid("You can only access your own orders.");
                }

                return Ok(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get order with details by ID
        /// </summary>
        [HttpGet("{id}/details")]
        public async Task<ActionResult<OrderDto>> GetOrderWithDetails(Guid id)
        {
            return await GetOrder(id); // Same logic as GetOrder
        }

        /// <summary>
        /// Get order by order number - Admin/Staff or order owner
        /// </summary>
        [HttpGet("number/{orderNumber}")]
        public async Task<ActionResult<OrderDto>> GetOrderByNumber(string orderNumber)
        {
            try
            {
                var order = await _orderService.GetOrderByNumberAsync(orderNumber);

                if (order == null)
                {
                    return NotFound($"Order with number {orderNumber} not found.");
                }

                var currentUserId = User.GetUserId();
                var currentUserRole = User.GetUserRole();

                // Allow if admin/staff or order owner
                if (currentUserRole != "Admin" && currentUserRole != "Staff" && order.UserId != currentUserId)
                {
                    return Forbid("You can only access your own orders.");
                }

                return Ok(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get orders by status - Admin/Staff only
        /// </summary>
        [HttpGet("status/{status}")]
        [AuthorizeRole("Admin", "Staff")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByStatus(string status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        /// <summary>
        /// Create a new order - Tự động gán userId từ token
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                // Tự động gán userId từ token
                var userId = User.GetUserId();
                createOrderDto.UserId = userId;

                var order = await _orderService.CreateOrderAsync(createOrderDto);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing order - Admin/Staff only
        /// </summary>
        [HttpPut("{id}")]
        [AuthorizeRole("Admin", "Staff")]
        public async Task<ActionResult<OrderDto>> UpdateOrder(Guid id, UpdateOrderDto updateOrderDto)
        {
            var order = await _orderService.UpdateOrderAsync(id, updateOrderDto);

            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            return Ok(order);
        }

        /// <summary>
        /// Delete an order - Admin only
        /// </summary>
        [HttpDelete("{id}")]
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderService.DeleteOrderAsync(id);

            if (!result)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
