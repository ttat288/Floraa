using Application.DTOs.Product;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Constants;
using Application.Common.Interfaces;

namespace Application.Features.Products.Commands;

public record CreateProductCommand(CreateProductRequest Request, Guid UserId) : IRequest<BaseResponse<ProductDto>>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate shop exists and user has permission
        var shop = await _unitOfWork.Shops.GetByIdAsync(request.Request.ShopId);
        if (shop == null)
        {
            throw new NotFoundException("Shop", request.Request.ShopId);
        }

        // Check if user owns the shop or is employee
        if (shop.UserId != request.UserId)
        {
            var isEmployee = await _unitOfWork.Employees.ExistsAsync(e => e.UserId == request.UserId && e.ShopId == request.Request.ShopId);
            if (!isEmployee)
            {
                throw new ForbiddenException("You don't have permission to create products for this shop");
            }
        }

        var product = new Product
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            Price = request.Request.Price,
            Quantity = request.Request.Stock, // Map Stock to Quantity
            ImageUrl = request.Request.ImageUrl,
            ShopId = request.Request.ShopId,
            ThemeId = request.Request.ThemeId,
            OccasionId = request.Request.OccasionId,
            ColorId = request.Request.ColorId,
            SubProductId = request.Request.SubProductId,
            Composition = request.Request.Composition
        };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate products list cache
        await _cacheService.RemoveByPatternAsync($"{CacheKeys.PRODUCTS_LIST}*", cancellationToken);

        // Load related entities for response
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
            ShopName = shop.Name,
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

        return BaseResponse<ProductDto>.SuccessResult(productDto, "Product created successfully");
    }
}
