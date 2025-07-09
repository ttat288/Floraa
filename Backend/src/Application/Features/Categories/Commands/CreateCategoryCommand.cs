using Application.DTOs.Category;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Commands;

public record CreateCategoryCommand(CreateCategoryRequest Request) : IRequest<BaseResponse<CategoryDto>>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Request.Name,
            Description = request.Request.Description
        };

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("categories:*", cancellationToken);

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };

        return BaseResponse<CategoryDto>.SuccessResult(categoryDto, "Category created successfully");
    }
}
