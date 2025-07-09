using Application.DTOs.Color;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Application.Common.Interfaces;
using Application.Common.Constants;

namespace Application.Features.Colors.Commands;

public record CreateColorCommand(CreateColorRequest Request) : IRequest<BaseResponse<ColorDto>>;

public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, BaseResponse<ColorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public CreateColorCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<ColorDto>> Handle(CreateColorCommand request, CancellationToken cancellationToken)
    {
        var color = new Color
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            IsActive = true
        };

        await _unitOfWork.Colors.AddAsync(color);
        await _unitOfWork.SaveChangesAsync();

        // Invalidate cache
        await _cacheService.RemoveByPatternAsync("colors:*", cancellationToken);

        var colorDto = new ColorDto
        {
            Id = color.Id,
            Name = color.Name,
            Description = color.Description,
            IsActive = color.IsActive,
            CreatedAt = color.CreatedAt,
            UpdatedAt = color.UpdatedAt
        };

        return BaseResponse<ColorDto>.SuccessResult(colorDto, "Color created successfully");
    }
}
