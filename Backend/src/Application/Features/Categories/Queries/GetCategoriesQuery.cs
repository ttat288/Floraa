using Application.DTOs.Category;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Queries;

public record GetCategoriesQuery() : IRequest<BaseResponse<List<CategoryDto>>>;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, BaseResponse<List<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = "categories:list";

        var cachedCategories = await _cacheService.GetAsync<List<CategoryDto>>(cacheKey, cancellationToken);
        if (cachedCategories != null)
        {
            return BaseResponse<List<CategoryDto>>.SuccessResult(cachedCategories, "Categories retrieved from cache");
        }

        var categories = await _unitOfWork.Categories.GetAllAsync();

        var categoryDtos = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        }).ToList();

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, categoryDtos, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<List<CategoryDto>>.SuccessResult(categoryDtos, "Categories retrieved successfully");
    }
}
