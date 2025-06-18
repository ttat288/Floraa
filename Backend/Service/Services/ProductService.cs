using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product>()
                .FindAsync(p => p.IsActive);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product>()
                .FindWithIncludeAsync(
                    p => p.IsActive && p.IsFeatured,
                    p => p.Category
                );
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products = await _unitOfWork.Repository<Product>()
                .FindWithIncludeAsync(
                    p => p.IsActive && p.CategoryId == categoryId,
                    p => p.Category
                );
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            var products = await _unitOfWork.Repository<Product>()
                .FindWithIncludeAsync(
                    p => p.IsActive && 
                         (p.ProductName.Contains(searchTerm) || 
                          p.Description!.Contains(searchTerm)),
                    p => p.Category
                );
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> GetProductWithCategoryAsync(Guid id)
        {
            var product = await _unitOfWork.Repository<Product>()
                .GetSingleWithIncludeAsync(
                    p => p.Id == id,
                    p => p.Category
                );
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            await _unitOfWork.Repository<Product>().AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (existingProduct == null)
                return null;

            _mapper.Map(updateProductDto, existingProduct);
            _unitOfWork.Repository<Product>().Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (product == null)
                return false;

            _unitOfWork.Repository<Product>().Remove(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
