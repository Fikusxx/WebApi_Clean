namespace Clean.WebApi.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder AddMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();

        return builder;
    }
}
