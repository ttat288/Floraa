using Application.DTOs.Occasion;
using Application.Features.Occasions.Commands;
using Application.Features.Occasions.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OccasionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OccasionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<List<OccasionDto>>>> GetOccasions([FromQuery] bool? isActive = null)
    {
        var query = new GetOccasionsQuery(isActive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<OccasionDto>>> GetOccasionById(Guid id)
    {
        var query = new GetOccasionByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<OccasionDto>>> CreateOccasion([FromBody] CreateOccasionRequest request)
    {
        var command = new CreateOccasionCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<OccasionDto>>> UpdateOccasion(Guid id, [FromBody] UpdateOccasionRequest request)
    {
        var command = new UpdateOccasionCommand(id, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse>> DeleteOccasion(Guid id)
    {
        var command = new DeleteOccasionCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
