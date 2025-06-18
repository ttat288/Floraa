using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> GetOrderWithDetailsAsync(Guid id)
        {
            var order = await _unitOfWork.Repository<Order>()
                .GetSingleWithIncludeAsync(
                    o => o.Id == id,
                    o => o.OrderDetails,
                    o => o.User
                );
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> GetOrderByNumberAsync(string orderNumber)
        {
            var order = await _unitOfWork.Repository<Order>()
                .GetSingleWithIncludeAsync(
                    o => o.OrderNumber == orderNumber,
                    o => o.OrderDetails,
                    o => o.User
                );
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(Guid userId)
        {
            var orders = await _unitOfWork.Repository<Order>()
                .FindWithIncludeAsync(
                    o => o.UserId == userId,
                    o => o.OrderDetails
                );
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status)
        {
            var orders = await _unitOfWork.Repository<Order>()
                .FindWithIncludeAsync(
                    o => o.Status == status,
                    o => o.OrderDetails,
                    o => o.User
                );
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var order = _mapper.Map<Order>(createOrderDto);
            
            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> UpdateOrderAsync(Guid id, UpdateOrderDto updateOrderDto)
        {
            var existingOrder = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            if (existingOrder == null)
                return null;

            _mapper.Map(updateOrderDto, existingOrder);
            _unitOfWork.Repository<Order>().Update(existingOrder);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderDto>(existingOrder);
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            if (order == null)
                return false;

            _unitOfWork.Repository<Order>().Remove(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
