using Microsoft.Extensions.DependencyInjection;
using Restaurant.Payment.Data.Contexts;
using System.Reflection;

namespace Restaurant.Payment.Data;

public static class Configuration
{

    private static readonly Assembly thisAssembly = typeof(Configuration).Assembly;

    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddDbContext<PaymentContext>(ServiceLifetime.Scoped);
        services.AddScoped(thisAssembly, "Repository");

        return services;
    }

}
