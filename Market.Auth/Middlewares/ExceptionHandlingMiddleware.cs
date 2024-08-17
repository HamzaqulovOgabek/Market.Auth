using Market.Auth.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Call the next middleware in the pipeline
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Entity not found by request");
            await HandleNotFoundExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred."); // Log the exception
            await HandleExceptionAsync(context, ex); // Handle the exception
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An internal server error occurred. Please try again later.",
            Detailed = exception.Message // Optional: Include the exception message for debugging
        };

        return context.Response.WriteAsJsonAsync(response);
    }

    private static Task HandleNotFoundExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Entity not found by request.",
            Detailed = exception.Message // Optional: Include the exception message for debugging
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
