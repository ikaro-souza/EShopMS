namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Inject Carter

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // map carter enpoints

        return app;
    }
}