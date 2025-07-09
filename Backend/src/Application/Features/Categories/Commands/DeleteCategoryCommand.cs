using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Commands;

public record DeleteCategoryCommand(Guid Id) : IRequest<BaseResponse>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category == null)
        {
            throw new NotFoundException("Category", request.Id);
        }

        // Check if category is being used by sub products
        var isUsed = await _unitOfWork.SubProducts.ExistsAsync(sp => sp.CategoryId == request.Id);
        if (isUsed)
        {
            throw new ValidationException(new[] { "Cannot delete category that is being used by sub products" });
        }

        _unitOfWork.Categories.Delete(category);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("categories:*", cancellationToken);

        return BaseResponse.SuccessResult("Category deleted successfully");
    }
}
