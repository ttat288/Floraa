using Application.DTOs.Occasion;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Occasions.Queries;

public record GetOccasionsQuery(bool? IsActive = null) : IRequest<BaseResponse<List<OccasionDto>>>;

public class GetOccasionsQueryHandler : IRequestHandler<GetOccasionsQuery, BaseResponse<List<OccasionDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetOccasionsQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<List<OccasionDto>>> Handle(GetOccasionsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"occasions:list:active:{request.IsActive}";

        var cachedOccasions = await _cacheService.GetAsync<List<OccasionDto>>(cacheKey, cancellationToken);
        if (cachedOccasions != null)
        {
            return BaseResponse<List<OccasionDto>>.SuccessResult(cachedOccasions, "Occasions retrieved from cache");
        }

        var occasions = await _unitOfWork.Occasions.GetAllAsync();

        if (request.IsActive.HasValue)
        {
            occasions = occasions.Where(o => o.IsActive == request.IsActive.Value);
        }

        var occasionDtos = occasions.Select(o => new OccasionDto
        {
            Id = o.Id,
            Name = o.Name,
            IsActive = o.IsActive,
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt
        }).ToList();

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, occasionDtos, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<List<OccasionDto>>.SuccessResult(occasionDtos, "Occasions retrieved successfully");
    }
}
