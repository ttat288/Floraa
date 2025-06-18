using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Repository<User>().GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> GetActiveUsersAsync()
        {
            var users = await _unitOfWork.Repository<User>()
                .FindAsync(u => u.IsActive);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.Repository<User>()
                .GetSingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var existingUser = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (existingUser == null)
                return null;

            _mapper.Map(updateUserDto, existingUser);
            _unitOfWork.Repository<User>().Update(existingUser);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
                return false;

            _unitOfWork.Repository<User>().Remove(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            return await _unitOfWork.Repository<User>().ExistsAsync(u => u.Id == id);
        }
    }
}
