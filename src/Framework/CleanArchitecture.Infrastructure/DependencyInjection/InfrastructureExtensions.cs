using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Ghanavats.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.DependencyInjection;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services) 
    {
        services.AddScoped(typeof(IRepository<>), typeof(MarkerRepository<>));
        
        return services;
    }
    
    public static IServiceCollection AddSqlDb(this IServiceCollection services, 
        IConfigurationSection configurationSection, 
        bool isDevelopment)
    {
        services.AddDbContextPool<PlayGroundDbContext>(options =>
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
