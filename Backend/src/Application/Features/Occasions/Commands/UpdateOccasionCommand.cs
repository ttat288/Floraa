using Application.DTOs.Occasion;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Occasions.Commands;

public record UpdateOccasionCommand(Guid Id, UpdateOccasionRequest Request) : IRequest<BaseResponse<OccasionDto>>;

public class UpdateOccasionCommandHandler : IRequestHandler<UpdateOccasionCommand, BaseResponse<OccasionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public UpdateOccasionCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<OccasionDto>> Handle(UpdateOccasionCommand request, CancellationToken cancellationToken)
    {
        var occasion = await _unitOfWork.Occasions.GetByIdAsync(request.Id);
        if (occasion == null)
        {
            throw new NotFoundException("Occasion", request.Id);
        }

        occasion.Name = request.Request.Name;
        occasion.IsActive = request.Request.IsActive;

        _unitOfWork.Occasions.Update(occasion);
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

        return BaseResponse<OccasionDto>.SuccessResult(occasionDto, "Occasion updated successfully");
    }
}
