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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users - Admin only
        /// </summary>
        [HttpGet]
        [AuthorizeRole("Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get active users only - Admin/Staff
        /// </summary>
        [HttpGet("active")]
        [AuthorizeRole("Admin", "Staff")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetActiveUsers()
        {
            var users = await _userService.GetActiveUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get user by ID - Admin/Staff or own profile
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            try
            {
                var currentUserId = User.GetUserId();
                var currentUserRole = User.GetUserRole();

                // Allow if admin/staff or accessing own profile
                if (currentUserRole != "Admin" && currentUserRole != "Staff" && currentUserId != id)
                {
                    return Forbid("You can only access your own profile.");
                }

                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get current user profile - Tự động lấy từ token
        /// </summary>
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetMyProfile()
        {
            try
            {
                var userId = User.GetUserId();
                var user = await _userService.GetUserByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User profile not found.");
                }

                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update current user profile - Tự động lấy userId từ token
        /// </summary>
        [HttpPut("profile")]
        public async Task<ActionResult<UserDto>> UpdateMyProfile(UpdateUserDto updateUserDto)
        {
            try
            {
                var userId = User.GetUserId();
                var currentUserRole = User.GetUserRole();

                // Prevent role escalation - only admin can change roles
                if (currentUserRole != "Admin")
                {
                    updateUserDto.Role = currentUserRole; // Keep current role
                }

                var user = await _userService.UpdateUserAsync(userId, updateUserDto);

                if (user == null)
                {
                    return NotFound("User profile not found.");
                }

                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get user by email - Admin/Staff only
        /// </summary>
        [HttpGet("email/{email}")]
        [AuthorizeRole("Admin", "Staff")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound($"User with email {email} not found.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Create a new user - Admin only
        /// </summary>
        [HttpPost]
        [AuthorizeRole("Admin")]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(createUserDto);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing user - Admin only
        /// </summary>
        [HttpPut("{id}")]
        [AuthorizeRole("Admin")]
        public async Task<ActionResult<UserDto>> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await _userService.UpdateUserAsync(id, updateUserDto);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Delete a user - Admin only
        /// </summary>
        [HttpDelete("{id}")]
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var currentUserId = User.GetUserId();

                // Prevent self-deletion
                if (currentUserId == id)
                {
                    return BadRequest(new { message = "You cannot delete your own account." });
                }

                var result = await _userService.DeleteUserAsync(id);

                if (!result)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Check if user exists - Admin/Staff only
        /// </summary>
        [HttpHead("{id}")]
        [AuthorizeRole("Admin", "Staff")]
        public async Task<IActionResult> UserExists(Guid id)
        {
            var exists = await _userService.UserExistsAsync(id);
            return exists ? Ok() : NotFound();
        }
    }
}
