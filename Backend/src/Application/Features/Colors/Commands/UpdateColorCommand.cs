using Application.DTOs.Color;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Features.Colors.Commands;

public record UpdateColorCommand(Guid Id, UpdateColorRequest Request) : IRequest<BaseResponse<ColorDto>>;

public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, BaseResponse<ColorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public UpdateColorCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<ColorDto>> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
    {
        var color = await _unitOfWork.Colors.GetByIdAsync(request.Id);
        if (color == null)
        {
            throw new NotFoundException("Color", request.Id);
        }

        color.Name = request.Request.Name;
        color.Description = request.Request.Description;
        color.IsActive = request.Request.IsActive;

        _unitOfWork.Colors.Update(color);
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

        return BaseResponse<ColorDto>.SuccessResult(colorDto, "Color updated successfully");
    }
}
