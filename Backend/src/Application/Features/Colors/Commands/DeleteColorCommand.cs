using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Colors.Commands;

public record DeleteColorCommand(Guid Id) : IRequest<BaseResponse>;

public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public DeleteColorCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
    {
        var color = await _unitOfWork.Colors.GetByIdAsync(request.Id);
        if (color == null)
        {
            throw new NotFoundException("Color", request.Id);
        }

        // Check if color is being used by products
        var isUsed = await _unitOfWork.Products.ExistsAsync(p => p.ColorId == request.Id);
        if (isUsed)
        {
            throw new ValidationException(new[] { "Cannot delete color that is being used by products" });
        }

        _unitOfWork.Colors.Delete(color);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("colors:*", cancellationToken);

        return BaseResponse.SuccessResult("Color deleted successfully");
    }
}
