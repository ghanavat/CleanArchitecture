using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Factories;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) 
    {
        services.AddScoped(typeof(IRepository<>), typeof(MarkerRepository<>));
        services.AddScoped(typeof(IDomainFactory<,>), typeof(CreateEntityObjectFactory<,>));
        
        return services;
    }
    
    public static IServiceCollection AddSqlDb(this IServiceCollection services, 
        IConfigurationSection configurationSection, 
        bool isDevelopment)
    {
        services.AddDbContextPool<PlayGroundDbContext>((options) =>
        {
            options.UseSqlServer(configurationSection["ConnectionString"]);
            
            if (isDevelopment)
            {
                options.EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }
        });

        return services;
    }
}
