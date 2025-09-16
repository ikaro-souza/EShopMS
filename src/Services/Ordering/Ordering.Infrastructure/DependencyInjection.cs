using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
        // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}