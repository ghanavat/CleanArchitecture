using CleanArchitecture.UseCases.Feature1.GetSomeDataForSomeId;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.UseCases;

public static class ServiceRegistration
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<GetSomeDataForSomeIdValidator>();
        return services;
    }
}
