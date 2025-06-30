using Application.DTOs.Product;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Constants;
using Application.Common.Interfaces;

namespace Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<BaseResponse<ProductDto>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BaseResponse<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.ProductById(request.Id);

        var cachedProduct = await _cacheService.GetAsync<ProductDto>(cacheKey, cancellationToken);
        if (cachedProduct != null)
        {
            return BaseResponse<ProductDto>.SuccessResult(cachedProduct, "Product retrieved from cache");
        }

        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException("Product", request.Id);
        }

        // Load related entities
        var shop = await _unitOfWork.Shops.GetByIdAsync(product.ShopId);
        var theme = product.ThemeId.HasValue ? await _unitOfWork.Themes.GetByIdAsync(product.ThemeId.Value) : null;
        var occasion = product.OccasionId.HasValue ? await _unitOfWork.Occasions.GetByIdAsync(product.OccasionId.Value) : null;
        var color = product.ColorId.HasValue ? await _unitOfWork.Colors.GetByIdAsync(product.ColorId.Value) : null;
        var subProduct = product.SubProductId.HasValue ? await _unitOfWork.SubProducts.GetByIdAsync(product.SubProductId.Value) : null;

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Quantity, // Map Quantity to Stock
            ImageUrl = product.ImageUrl,
            ShopId = product.ShopId,
            ShopName = shop?.Name ?? "Unknown",
            ThemeId = product.ThemeId,
            ThemeName = theme?.Name,
            OccasionId = product.OccasionId,
            OccasionName = occasion?.Name,
            ColorId = product.ColorId,
            ColorName = color?.Name,
            SubProductId = product.SubProductId,
            SubProductMeaning = subProduct?.Meaning,
            Composition = product.Composition,
            SoldCount = product.SoldCount,
            CreatedAt = product.CreatedAt
        };

        // Cache the result
        await _cacheService.SetAsync(cacheKey, productDto, CacheSettings.ProductCacheExpiry, cancellationToken);

        return BaseResponse<ProductDto>.SuccessResult(productDto, "Product retrieved successfully");
    }
}
