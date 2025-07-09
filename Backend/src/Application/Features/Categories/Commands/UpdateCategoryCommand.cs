using Application.DTOs.Category;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Commands;

public record UpdateCategoryCommand(Guid Id, UpdateCategoryRequest Request) : IRequest<BaseResponse<CategoryDto>>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseResponse<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category == null)
        {
            throw new NotFoundException("Category", request.Id);
        }

        category.Name = request.Request.Name;
        category.Description = request.Request.Description;

        _unitOfWork.Categories.Update(category);
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

        return BaseResponse<CategoryDto>.SuccessResult(categoryDto, "Category updated successfully");
    }
}
