using LiquidLabAssesment.DTOs.Responses;

namespace LiquidLabAssesment.Configurations.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            _logger.LogError(ex.Message);
            await HandleErrorAsync(context, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = 500;

        CustomHttpResponse response = new CustomHttpResponse()
        {
            Message = exception.Message,
            StatusCode = 400
        };
        
        await context.Response.WriteAsJsonAsync(response);
    }
}