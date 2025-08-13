namespace WebApiAuthentication.Api;

public class ApiKeyValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var config = context.RequestServices.GetRequiredService<IConfiguration>();
        var validApiKey = config["Authentication:ApiKey"];

        if (string.IsNullOrWhiteSpace(validApiKey))
        {
            throw new KeyNotFoundException("The API key was not found in the configuration.");
        }

        if (!context.Request.Headers.TryGetValue("X-ApiKey", out var extractedApiKey))
        {
            if (!context.Request.Query.TryGetValue("X-ApiKey", out extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API key was not provided.");
                return;
            }
        }

        if (!validApiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API key is invalid.");
            return;
        }
        
        await _next(context);
    }
}