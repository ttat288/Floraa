using Application.DTOs.Theme;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Themes.Commands;

public record UpdateThemeCommand(Guid Id, UpdateThemeRequest Request) : IRequest<BaseResponse<ThemeDto>>;

public class UpdateThemeCommandHandler : IRequestHandler<UpdateThemeCommand, BaseResponse<ThemeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public UpdateThemeCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<ThemeDto>> Handle(UpdateThemeCommand request, CancellationToken cancellationToken)
    {
        var theme = await _unitOfWork.Themes.GetByIdAsync(request.Id);
        if (theme == null)
        {
            throw new NotFoundException("Theme", request.Id);
        }

        theme.Name = request.Request.Name;
        theme.Description = request.Request.Description;
        theme.IsActive = request.Request.IsActive;

        _unitOfWork.Themes.Update(theme);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("themes:*", cancellationToken);

        var themeDto = new ThemeDto
        {
            Id = theme.Id,
            Name = theme.Name,
            Description = theme.Description,
            IsActive = theme.IsActive,
            CreatedAt = theme.CreatedAt,
            UpdatedAt = theme.UpdatedAt
        };

        return BaseResponse<ThemeDto>.SuccessResult(themeDto, "Theme updated successfully");
    }
}
