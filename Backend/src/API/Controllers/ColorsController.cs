using Application.DTOs.Color;
using Application.Features.Colors.Commands;
using Application.Features.Colors.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ColorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<List<ColorDto>>>> GetColors([FromQuery] bool? isActive = null)
    {
        var query = new GetColorsQuery(isActive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<ColorDto>>> GetColorById(Guid id)
    {
        var query = new GetColorByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<ColorDto>>> CreateColor([FromBody] CreateColorRequest request)
    {
        var command = new CreateColorCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<ColorDto>>> UpdateColor(Guid id, [FromBody] UpdateColorRequest request)
    {
        var command = new UpdateColorCommand(id, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse>> DeleteColor(Guid id)
    {
        var command = new DeleteColorCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
