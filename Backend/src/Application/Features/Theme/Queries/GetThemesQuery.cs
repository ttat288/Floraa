using Application.DTOs.Theme;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Themes.Queries;

public record GetThemesQuery(bool? IsActive = null) : IRequest<BaseResponse<List<ThemeDto>>>;

public class GetThemesQueryHandler : IRequestHandler<GetThemesQuery, BaseResponse<List<ThemeDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetThemesQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<List<ThemeDto>>> Handle(GetThemesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"themes:list:active:{request.IsActive}";

        var cachedThemes = await _cacheService.GetAsync<List<ThemeDto>>(cacheKey, cancellationToken);
        if (cachedThemes != null)
        {
            return BaseResponse<List<ThemeDto>>.SuccessResult(cachedThemes, "Themes retrieved from cache");
        }

        var themes = await _unitOfWork.Themes.GetAllAsync();

        if (request.IsActive.HasValue)
        {
            themes = themes.Where(t => t.IsActive == request.IsActive.Value);
        }

        var themeDtos = themes.Select(t => new ThemeDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            IsActive = t.IsActive,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        }).ToList();

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, themeDtos, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<List<ThemeDto>>.SuccessResult(themeDtos, "Themes retrieved successfully");
    }
}
