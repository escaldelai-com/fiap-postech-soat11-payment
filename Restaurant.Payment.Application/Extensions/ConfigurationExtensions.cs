using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Restaurant.Payment;

public static class ConfigurationExtensions
{

    public static IServiceCollection AddScoped(this IServiceCollection services, Assembly assembly, string serviceSuffix)
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith(serviceSuffix));

        foreach (var implementationType in types)
        {
            var interfaces = implementationType.GetInterfaces();

            foreach (var serviceType in interfaces)
                services.AddScoped(serviceType, implementationType);
        }

        return services;
    }

    public static IServiceCollection AddTransient(this IServiceCollection services, Assembly assembly, string serviceSuffix)
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith(serviceSuffix));

        foreach (var implementationType in types)
        {
            var interfaces = implementationType.GetInterfaces();

            foreach (var serviceType in interfaces)
                services.AddTransient(serviceType, implementationType);
        }

        return services;
    }

    public static IServiceCollection AddSingleton(this IServiceCollection services, Assembly assembly, string serviceSuffix)
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith(serviceSuffix));

        foreach (var implementationType in types)
        {
            var interfaces = implementationType.GetInterfaces();

            foreach (var serviceType in interfaces)
                services.AddSingleton(serviceType, implementationType);
        }

        return services;
    }

}