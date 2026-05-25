using System.Net;
using System.Text.Json;
using ParkingApp.Domain.Common;

namespace ParkingApp.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        if (exception is NotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
        }

        if (exception is BadRequestException)
        {
            statusCode = HttpStatusCode.BadRequest;
        }

        var response = new
        {
            message = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}