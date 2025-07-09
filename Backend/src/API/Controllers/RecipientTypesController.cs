using Application.DTOs.RecipientType;
using Application.Features.RecipientTypes.Commands;
using Application.Features.RecipientTypes.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipientTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecipientTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<List<RecipientTypeDto>>>> GetRecipientTypes([FromQuery] bool? isActive = null)
    {
        var query = new GetRecipientTypesQuery(isActive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<RecipientTypeDto>>> CreateRecipientType([FromBody] CreateRecipientTypeRequest request)
    {
        var command = new CreateRecipientTypeCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
