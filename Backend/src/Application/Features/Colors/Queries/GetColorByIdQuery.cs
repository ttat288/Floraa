using Application.DTOs.Color;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Colors.Queries;

public record GetColorByIdQuery(Guid Id) : IRequest<BaseResponse<ColorDto>>;

public class GetColorByIdQueryHandler : IRequestHandler<GetColorByIdQuery, BaseResponse<ColorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetColorByIdQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<ColorDto>> Handle(GetColorByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"colors:id:{request.Id}";

        var cachedColor = await _cacheService.GetAsync<ColorDto>(cacheKey, cancellationToken);
        if (cachedColor != null)
        {
            return BaseResponse<ColorDto>.SuccessResult(cachedColor, "Color retrieved from cache");
        }

        var color = await _unitOfWork.Colors.GetByIdAsync(request.Id);
        if (color == null)
        {
            throw new NotFoundException("Color", request.Id);
        }

        var colorDto = new ColorDto
        {
            Id = color.Id,
            Name = color.Name,
            Description = color.Description,
            IsActive = color.IsActive,
            CreatedAt = color.CreatedAt,
            UpdatedAt = color.UpdatedAt
        };

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, colorDto, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<ColorDto>.SuccessResult(colorDto, "Color retrieved successfully");
    }
}
