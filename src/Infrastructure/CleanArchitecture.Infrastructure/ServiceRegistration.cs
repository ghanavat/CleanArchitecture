using System.Reflection;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Helpers;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace CleanArchitecture.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) 
    {
        services.AddScoped(typeof(IRepository<>), typeof(MarkerRepository<>));
        
        return services;
    }

    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfigurationSection configurationSection)
    {
        var mongoClient = new MongoClient(configurationSection["ConnectionString"]);
        services.AddDbContextPool<SampleDbContext>((services, options) =>
        {
            options.UseMongoDB(mongoClient, "PlayGround");
        });

        return services;
    }
}
