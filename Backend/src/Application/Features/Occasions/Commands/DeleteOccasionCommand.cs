using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Occasions.Commands;

public record DeleteOccasionCommand(Guid Id) : IRequest<BaseResponse>;

public class DeleteOccasionCommandHandler : IRequestHandler<DeleteOccasionCommand, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public DeleteOccasionCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse> Handle(DeleteOccasionCommand request, CancellationToken cancellationToken)
    {
        var occasion = await _unitOfWork.Occasions.GetByIdAsync(request.Id);
        if (occasion == null)
        {
            throw new NotFoundException("Occasion", request.Id);
        }

        // Check if occasion is being used by products
        var isUsed = await _unitOfWork.Products.ExistsAsync(p => p.OccasionId == request.Id);
        if (isUsed)
        {
            throw new ValidationException(new[] { "Cannot delete occasion that is being used by products" });
        }

        _unitOfWork.Occasions.Delete(occasion);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("occasions:*", cancellationToken);

        return BaseResponse.SuccessResult("Occasion deleted successfully");
    }
}
