using Service.DTOs;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetActiveProductsAsync();
        Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync();
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(Guid categoryId);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<ProductDto?> GetProductWithCategoryAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
