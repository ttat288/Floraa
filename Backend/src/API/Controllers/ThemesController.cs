using Application.DTOs.Theme;
using Application.Features.Themes.Commands;
using Application.Features.Themes.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ThemesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ThemesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<List<ThemeDto>>>> GetThemes([FromQuery] bool? isActive = null)
    {
        var query = new GetThemesQuery(isActive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<ThemeDto>>> GetThemeById(Guid id)
    {
        var query = new GetThemeByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<ThemeDto>>> CreateTheme([FromBody] CreateThemeRequest request)
    {
        var command = new CreateThemeCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<ThemeDto>>> UpdateTheme(Guid id, [FromBody] UpdateThemeRequest request)
    {
        var command = new UpdateThemeCommand(id, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse>> DeleteTheme(Guid id)
    {
        var command = new DeleteThemeCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
