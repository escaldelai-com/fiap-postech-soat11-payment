using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Restaurant.Payment.Presenter;

public static class Configuration
{

    private static readonly Assembly thisAssembly = typeof(Configuration).Assembly;

    public static IServiceCollection AddPresenter(this IServiceCollection services)
    {
        services.AddAutoMapper(thisAssembly);
        services.AddSingleton(thisAssembly, "Presenter");

        return services;
    }

}
