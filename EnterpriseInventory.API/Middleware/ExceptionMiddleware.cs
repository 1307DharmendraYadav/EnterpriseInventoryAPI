using System.Net;
using System.Text.Json;
using EnterpriseInventory.Application.Common;
using EnterpriseInventory.Application.Exceptions;

namespace EnterpriseInventory.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }

        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.NotFound,
                ex.Message);
        }

        catch (ConflictException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.Conflict,
                ex.Message);
        }

        catch (UnauthorizedException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.Unauthorized,
                ex.Message);
        }

        catch (ForbiddenException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.Forbidden,
                ex.Message);
        }

        catch (Exception ex)
        {
            _logger.LogError
                (
                    ex,
                    "Unhandled exception occurred while processing {Method} {Path}. TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.TraceIdentifier
                );

            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred."
                );
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context,
        HttpStatusCode statusCode,string message)
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = (int)statusCode;

        var response = new ApiResponse<object>
        {
            Success = false,
            StatusCode = (int)statusCode,
            Message = message,
            Errors = new List<string> { message },
            TraceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}