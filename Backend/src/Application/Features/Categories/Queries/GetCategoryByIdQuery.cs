using Application.DTOs.Category;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<BaseResponse<CategoryDto>>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, BaseResponse<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"categories:id:{request.Id}";

        var cachedCategory = await _cacheService.GetAsync<CategoryDto>(cacheKey, cancellationToken);
        if (cachedCategory != null)
        {
            return BaseResponse<CategoryDto>.SuccessResult(cachedCategory, "Category retrieved from cache");
        }

        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category == null)
        {
            throw new NotFoundException("Category", request.Id);
        }

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, categoryDto, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<CategoryDto>.SuccessResult(categoryDto, "Category retrieved successfully");
    }
}
