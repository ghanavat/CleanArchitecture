using CleanArchitecture.UseCases.PlayerFeature.Create;
using CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.UseCases;

public static class ServiceRegistration
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<GetSomeDataForSomeIdValidator>();
        services.AddValidatorsFromAssemblyContaining<CreatePlayerCommandValidator>();
        return services;
    }
}
