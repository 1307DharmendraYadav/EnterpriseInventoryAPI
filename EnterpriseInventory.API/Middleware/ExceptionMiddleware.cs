using EnterpriseInventory.API.Models;
using EnterpriseInventory.Application.Exceptions;
using System.Net;
using System.Text.Json;

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
        catch (Exception ex)
        {
            var (statusCode, message, logLevel) = MapException(ex);

            LogException(context, ex, logLevel);

            await HandleExceptionAsync(
                context,
                statusCode,
                message);
        }
    }

    private static (HttpStatusCode StatusCode,string Message,LogLevel LogLevel) MapException(Exception exception)
    {
        return exception switch
        {
            ValidationException =>
                (HttpStatusCode.BadRequest,
                 exception.Message,
                 LogLevel.Warning),

            NotFoundException =>
                (HttpStatusCode.NotFound,
                 exception.Message,
                 LogLevel.Warning),

            ConflictException =>
                (HttpStatusCode.Conflict,
                 exception.Message,
                 LogLevel.Warning),

            UnauthorizedException =>
                (HttpStatusCode.Unauthorized,
                 exception.Message,
                 LogLevel.Warning),

            ForbiddenException =>
                (HttpStatusCode.Forbidden,
                 exception.Message,
                 LogLevel.Warning),

            // Default case: handles all unexpected/unmapped exceptions
            _ =>
                (HttpStatusCode.InternalServerError,
                 "An unexpected error occurred.",
                 LogLevel.Error)
        };
    }

    private void LogException(HttpContext context,Exception exception,LogLevel logLevel)
    {
        _logger.Log(
            logLevel,
            exception,
            "Exception occurred while processing {Method} {Path}. TraceId: {TraceId}",
            context.Request.Method,
            context.Request.Path,
            context.TraceIdentifier);
    }

    private static async Task HandleExceptionAsync(HttpContext context,HttpStatusCode statusCode,string message)
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