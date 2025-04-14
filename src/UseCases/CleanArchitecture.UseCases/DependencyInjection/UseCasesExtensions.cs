using CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.UseCases.DependencyInjection;

public static class UseCasesExtensions
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<GetSomeDataForSomeIdValidator>();
        return services;
    }
}
