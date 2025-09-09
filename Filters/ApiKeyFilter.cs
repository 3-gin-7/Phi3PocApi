namespace Phi3PocApi.Filters;

public class ApiKeyFilter : IEndpointFilter
{
    private readonly string _expectedKey;

    public ApiKeyFilter(string expectedKey)
    {
        _expectedKey = expectedKey;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;

        if (!httpContext.Request.Headers.TryGetValue("X-Api-Key", out var providedKey) ||
            providedKey != _expectedKey)
        {
            return Results.Unauthorized();
        }

        return await next(context);
    }
}