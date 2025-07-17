using Application.DTOs.Auth;
using Application.Features.Auth.Commands;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("invite")]
    [Authorize(Roles = "SuperManager,Admin")]
    public async Task<ActionResult<BaseResponse<string>>> InviteUser([FromBody] RegisterRoleAccount request)
    {
        // Validate role permissions
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (currentUserRole == "SuperManager" && request.Role == EmployeeRole.Admin)
        {
            return BadRequest(BaseResponse<string>.ErrorResult("SuperManager cannot create Admin accounts"));
        }
        var command = new RegisterRoleCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("complete")]
    [AllowAnonymous]
    public async Task<ActionResult<BaseResponse<AuthResponse>>> CompleteAccount([FromBody] CompleteAccountRequest request)
    {
        var command = new CompleteAccountCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
