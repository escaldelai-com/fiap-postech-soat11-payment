using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Reflection;

namespace Restaurant.Payment.Data;

public static class Configuration
{

    private static readonly Assembly thisAssembly = typeof(Configuration).Assembly;

    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddMongoDb();
        services.AddScoped(thisAssembly, "Repository");

        return services;
    }


    private static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddScoped<IMongoClient>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("mongodb");

            return new MongoClient(connectionString);
        });

        services.AddScoped(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var client = provider.GetRequiredService<IMongoClient>();
            var database = configuration["database"];

            return client.GetDatabase(database);
        });

        return services;
    }


}
