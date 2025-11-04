using System.Net;
using InventoryApp.Application.Common.Exceptions;

namespace InventoryApp.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            Console.WriteLine("--> Middleware is working");
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";

        switch (ex)
        {
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = ex.Message;
                break;
            case BadRequestException:
                statusCode = HttpStatusCode.BadRequest;
                message = ex.Message;
                break;
            default:
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(new { error = message });
    }
}
