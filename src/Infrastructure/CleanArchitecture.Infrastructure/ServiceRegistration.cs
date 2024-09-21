using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Factories;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CleanArchitecture.Infrastructure;

#pragma warning disable CS1591
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) 
    {
        services.AddScoped(typeof(IRepository<>), typeof(MarkerRepository<>));
        services.AddScoped(typeof(IDomainFactory<,>), typeof(CreateEntityObjectFactory<,>));
        
        return services;
    }

    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfigurationSection configurationSection)
    {
        var mongoClient = new MongoClient(configurationSection["ConnectionString"]);
        services.AddDbContextPool<SampleDbContext>((serviceCollections, options) =>
        {
            options.UseMongoDB(mongoClient, "PlayGround");
        });

        return services;
    }
}
