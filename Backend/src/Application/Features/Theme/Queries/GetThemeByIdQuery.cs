using Application.DTOs.Theme;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Themes.Queries;

public record GetThemeByIdQuery(Guid Id) : IRequest<BaseResponse<ThemeDto>>;

public class GetThemeByIdQueryHandler : IRequestHandler<GetThemeByIdQuery, BaseResponse<ThemeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetThemeByIdQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<ThemeDto>> Handle(GetThemeByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"themes:id:{request.Id}";

        var cachedTheme = await _cacheService.GetAsync<ThemeDto>(cacheKey, cancellationToken);
        if (cachedTheme != null)
        {
            return BaseResponse<ThemeDto>.SuccessResult(cachedTheme, "Theme retrieved from cache");
        }

        var theme = await _unitOfWork.Themes.GetByIdAsync(request.Id);
        if (theme == null)
        {
            throw new NotFoundException("Theme", request.Id);
        }

        var themeDto = new ThemeDto
        {
            Id = theme.Id,
            Name = theme.Name,
            Description = theme.Description,
            IsActive = theme.IsActive,
            CreatedAt = theme.CreatedAt,
            UpdatedAt = theme.UpdatedAt
        };

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, themeDto, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<ThemeDto>.SuccessResult(themeDto, "Theme retrieved successfully");
    }
}
