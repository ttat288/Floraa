using Application.DTOs.Color;
using Application.DTOs.Theme;
using Application.DTOs.Occasion;
using Application.DTOs.Category;
using Application.DTOs.RecipientType;
using Application.Features.Colors.Queries;
using Application.Features.Themes.Queries;
using Application.Features.Occasions.Queries;
using Application.Features.Categories.Queries;
using Application.Features.RecipientTypes.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MasterDataController : ControllerBase
{
    private readonly IMediator _mediator;

    public MasterDataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<ActionResult<BaseResponse<object>>> GetAllMasterData()
    {
        var colors = await _mediator.Send(new GetColorsQuery(true));
        var themes = await _mediator.Send(new GetThemesQuery(true));
        var occasions = await _mediator.Send(new GetOccasionsQuery(true));
        var categories = await _mediator.Send(new GetCategoriesQuery());
        var recipientTypes = await _mediator.Send(new GetRecipientTypesQuery(true));

        var masterData = new
        {
            Colors = colors.Data,
            Themes = themes.Data,
            Occasions = occasions.Data,
            Categories = categories.Data,
            RecipientTypes = recipientTypes.Data
        };

        return Ok(BaseResponse<object>.SuccessResult(masterData, "Master data retrieved successfully"));
    }

    [HttpGet("colors")]
    public async Task<ActionResult<BaseResponse<List<ColorDto>>>> GetColors()
    {
        var query = new GetColorsQuery(true);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("themes")]
    public async Task<ActionResult<BaseResponse<List<ThemeDto>>>> GetThemes()
    {
        var query = new GetThemesQuery(true);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("occasions")]
    public async Task<ActionResult<BaseResponse<List<OccasionDto>>>> GetOccasions()
    {
        var query = new GetOccasionsQuery(true);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("categories")]
    public async Task<ActionResult<BaseResponse<List<CategoryDto>>>> GetCategories()
    {
        var query = new GetCategoriesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("recipient-types")]
    public async Task<ActionResult<BaseResponse<List<RecipientTypeDto>>>> GetRecipientTypes()
    {
        var query = new GetRecipientTypesQuery(true);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
