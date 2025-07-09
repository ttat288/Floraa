using Application.DTOs.RecipientType;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.RecipientTypes.Queries;

public record GetRecipientTypesQuery(bool? IsActive = null) : IRequest<BaseResponse<List<RecipientTypeDto>>>;

public class GetRecipientTypesQueryHandler : IRequestHandler<GetRecipientTypesQuery, BaseResponse<List<RecipientTypeDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public GetRecipientTypesQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<List<RecipientTypeDto>>> Handle(GetRecipientTypesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"recipient-types:list:active:{request.IsActive}";

        var cachedRecipientTypes = await _cacheService.GetAsync<List<RecipientTypeDto>>(cacheKey, cancellationToken);
        if (cachedRecipientTypes != null)
        {
            return BaseResponse<List<RecipientTypeDto>>.SuccessResult(cachedRecipientTypes, "Recipient types retrieved from cache");
        }

        var recipientTypes = await _unitOfWork.RecipientTypes.GetAllAsync();

        if (request.IsActive.HasValue)
        {
            recipientTypes = recipientTypes.Where(rt => rt.IsActive == request.IsActive.Value);
        }

        var recipientTypeDtos = recipientTypes.Select(rt => new RecipientTypeDto
        {
            Id = rt.Id,
            Name = rt.Name,
            IsActive = rt.IsActive,
            CreatedAt = rt.CreatedAt,
            UpdatedAt = rt.UpdatedAt
        }).ToList();

        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, recipientTypeDtos, TimeSpan.FromHours(1), cancellationToken);

        return BaseResponse<List<RecipientTypeDto>>.SuccessResult(recipientTypeDtos, "Recipient types retrieved successfully");
    }
}
