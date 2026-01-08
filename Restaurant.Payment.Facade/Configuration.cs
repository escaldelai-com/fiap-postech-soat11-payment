using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Restaurant.Payment.Facade;

public static class Configuration
{

    private static readonly Assembly thisAssembly = typeof(Configuration).Assembly;

    public static IServiceCollection AddFacade(this IServiceCollection services)
    {
        services.AddScoped(thisAssembly, "Facade");

        return services;
    }

}
