using Application.DTOs.Color;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Colors.Queries;

public record GetColorsQuery(bool? IsActive = null) : IRequest<BaseResponse<List<ColorDto>>>;

public class GetColorsQueryHandler : IRequestHandler<GetColorsQuery, BaseResponse<List<ColorDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetColorsQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<List<ColorDto>>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"colors:list:active:{request.IsActive}";

        var cachedColors = await _cacheService.GetAsync<List<ColorDto>>(cacheKey, cancellationToken);
        if (cachedColors != null)
        {
            return BaseResponse<List<ColorDto>>.SuccessResult(cachedColors, "Colors retrieved from cache");
        }

        var colors = await _unitOfWork.Colors.GetAllAsync();

        if (request.IsActive.HasValue)
        {
            colors = colors.Where(c => c.IsActive == request.IsActive.Value);
        }

        var colorDtos = colors.Select(c => new ColorDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        }).ToList();

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, colorDtos, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<List<ColorDto>>.SuccessResult(colorDtos, "Colors retrieved successfully");
    }
}
