using Microsoft.AspNetCore.Mvc;
using System.Net;
using Progi.BidCalculator.Core.Exceptions;

namespace Progi.BidCalculator.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        (HttpStatusCode statusCode, string message) = exception switch
        {
            ResourceNotFoundException => (HttpStatusCode.NotFound, exception.Message),
            ValidationException => (HttpStatusCode.UnprocessableEntity, exception.Message),
            BusinessException => (HttpStatusCode.BadRequest, exception.Message),
            _ => (HttpStatusCode.InternalServerError, env.IsProduction() ? "Oops. Something went wrong..." : exception.ToString())
        };
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var result = new ProblemDetails
        {
            Status = (int)statusCode,
            Type = exception.GetType().Name,
            Title = "An unexpected error occurred",
            Detail = message,
            Instance = $"{context.Request.Method} {context.Request.Path}"
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(result);
    }
}