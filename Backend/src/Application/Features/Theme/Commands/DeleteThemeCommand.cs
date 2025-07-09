using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Themes.Commands;

public record DeleteThemeCommand(Guid Id) : IRequest<BaseResponse>;

public class DeleteThemeCommandHandler : IRequestHandler<DeleteThemeCommand, BaseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public DeleteThemeCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse> Handle(DeleteThemeCommand request, CancellationToken cancellationToken)
    {
        var theme = await _unitOfWork.Themes.GetByIdAsync(request.Id);
        if (theme == null)
        {
            throw new NotFoundException("Theme", request.Id);
        }

        // Check if theme is being used by products
        var isUsed = await _unitOfWork.Products.ExistsAsync(p => p.ThemeId == request.Id);
        if (isUsed)
        {
            throw new ValidationException(new[] { "Cannot delete theme that is being used by products" });
        }

        _unitOfWork.Themes.Delete(theme);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("themes:*", cancellationToken);

        return BaseResponse.SuccessResult("Theme deleted successfully");
    }
}
