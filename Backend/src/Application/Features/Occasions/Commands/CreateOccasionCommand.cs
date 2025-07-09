using Application.DTOs.Occasion;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Occasions.Commands;

public record CreateOccasionCommand(CreateOccasionRequest Request) : IRequest<BaseResponse<OccasionDto>>;

public class CreateOccasionCommandHandler : IRequestHandler<CreateOccasionCommand, BaseResponse<OccasionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public CreateOccasionCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<OccasionDto>> Handle(CreateOccasionCommand request, CancellationToken cancellationToken)
    {
        var occasion = new Occasion
        {
            Name = request.Request.Name,
            IsActive = true
        };

        await _unitOfWork.Occasions.AddAsync(occasion);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("occasions:*", cancellationToken);

        var occasionDto = new OccasionDto
        {
            Id = occasion.Id,
            Name = occasion.Name,
            IsActive = occasion.IsActive,
            CreatedAt = occasion.CreatedAt,
            UpdatedAt = occasion.UpdatedAt
        };

        return BaseResponse<OccasionDto>.SuccessResult(occasionDto, "Occasion created successfully");
    }
}
