using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Restaurant.Payment.ExternalServices;

public static class Configuration
{

    private static readonly Assembly thisAssembly = typeof(Configuration).Assembly;

    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped(thisAssembly, "Service");

        return services;
    }

}
