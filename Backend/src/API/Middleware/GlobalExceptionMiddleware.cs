using Application.Common.Exceptions;
using Domain.Common;
using System.Net;
using System.Text.Json;

namespace API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            NotFoundException => new BaseResponse
            {
                Success = false,
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.NotFound
            },
            ValidationException validationEx => new BaseResponse
            {
                Success = false,
                Message = validationEx.Message,
                Errors = validationEx.Errors,
                StatusCode = (int)HttpStatusCode.BadRequest
            },
            UnauthorizedException => new BaseResponse
            {
                Success = false,
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.Unauthorized
            },
            ForbiddenException => new BaseResponse
            {
                Success = false,
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.Forbidden
            },
            _ => new BaseResponse
            {
                Success = false,
                Message = "An error occurred while processing your request",
                StatusCode = (int)HttpStatusCode.InternalServerError
            }
        };

        context.Response.StatusCode = response.StatusCode;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
