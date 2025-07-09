using Application.DTOs.Category;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<List<CategoryDto>>>> GetCategories()
    {
        var query = new GetCategoriesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<CategoryDto>>> GetCategoryById(Guid id)
    {
        var query = new GetCategoryByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<CategoryDto>>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse<CategoryDto>>> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand(id, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BaseResponse>> DeleteCategory(Guid id)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
