using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Ghanavats.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.DependencyInjection;

public static class InfrastructureExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddRepository() 
        {
            services.AddScoped(typeof(IRepository<>), typeof(MarkerRepository<>));
        
            return services;
        }

        public IServiceCollection AddSqlDb(IConfigurationSection configurationSection, 
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
}
