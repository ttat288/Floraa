using Application.DTOs.RecipientType;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.RecipientTypes.Commands;

public record CreateRecipientTypeCommand(CreateRecipientTypeRequest Request) : IRequest<BaseResponse<RecipientTypeDto>>;

public class CreateRecipientTypeCommandHandler : IRequestHandler<CreateRecipientTypeCommand, BaseResponse<RecipientTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public CreateRecipientTypeCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<RecipientTypeDto>> Handle(CreateRecipientTypeCommand request, CancellationToken cancellationToken)
    {
        var recipientType = new RecipientType
        {
            Name = request.Request.Name,
            IsActive = true
        };

        await _unitOfWork.RecipientTypes.AddAsync(recipientType);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("recipient-types:*", cancellationToken);

        var recipientTypeDto = new RecipientTypeDto
        {
            Id = recipientType.Id,
            Name = recipientType.Name,
            IsActive = recipientType.IsActive,
            CreatedAt = recipientType.CreatedAt,
            UpdatedAt = recipientType.UpdatedAt
        };

        return BaseResponse<RecipientTypeDto>.SuccessResult(recipientTypeDto, "Recipient type created successfully");
    }
}
