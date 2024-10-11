using CleanArchitecture.Shared.DomainEventMechanism;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Shared;

public static class ServiceRegistration
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();

        return services;
    }
}
