using Application.DTOs.Theme;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Themes.Commands;

public record CreateThemeCommand(CreateThemeRequest Request) : IRequest<BaseResponse<ThemeDto>>;

public class CreateThemeCommandHandler : IRequestHandler<CreateThemeCommand, BaseResponse<ThemeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public CreateThemeCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<ThemeDto>> Handle(CreateThemeCommand request, CancellationToken cancellationToken)
    {
        var theme = new Theme
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            IsActive = true
        };

        await _unitOfWork.Themes.AddAsync(theme);
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

        return BaseResponse<ThemeDto>.SuccessResult(themeDto, "Theme created successfully");
    }
}
