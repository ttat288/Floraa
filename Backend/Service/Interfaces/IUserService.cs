using Service.DTOs;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<IEnumerable<UserDto>> GetActiveUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> UserExistsAsync(Guid id);
    }
}
