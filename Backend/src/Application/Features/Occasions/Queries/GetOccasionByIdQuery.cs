using Application.DTOs.Occasion;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Occasions.Queries;

public record GetOccasionByIdQuery(Guid Id) : IRequest<BaseResponse<OccasionDto>>;

public class GetOccasionByIdQueryHandler : IRequestHandler<GetOccasionByIdQuery, BaseResponse<OccasionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetOccasionByIdQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<OccasionDto>> Handle(GetOccasionByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"occasions:id:{request.Id}";

        var cachedOccasion = await _cacheService.GetAsync<OccasionDto>(cacheKey, cancellationToken);
        if (cachedOccasion != null)
        {
            return BaseResponse<OccasionDto>.SuccessResult(cachedOccasion, "Occasion retrieved from cache");
        }

        var occasion = await _unitOfWork.Occasions.GetByIdAsync(request.Id);
        if (occasion == null)
        {
            throw new NotFoundException("Occasion", request.Id);
        }

        var occasionDto = new OccasionDto
        {
            Id = occasion.Id,
            Name = occasion.Name,
            IsActive = occasion.IsActive,
            CreatedAt = occasion.CreatedAt,
            UpdatedAt = occasion.UpdatedAt
        };

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, occasionDto, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<OccasionDto>.SuccessResult(occasionDto, "Occasion retrieved successfully");
    }
}
