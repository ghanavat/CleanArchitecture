using CleanArchitecture.Shared.Extensions;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.UseCases;

/// <summary>
/// Validation Behaviour that is triggered by MediatR pipeline for every API request
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidatorBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Validation Behaviour constructor
    /// </summary>
    /// <param name="validators"></param>
    public ValidatorBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators.CheckForNull();
    }

    /// <summary>
    /// Implementation of IPipelineBehaviour to handle validation errors.
    /// </summary>
    /// <param name="request">The request model that is to be validated</param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);
        var validationFailures = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        if (validationFailures.Length <= 0) return await next(cancellationToken);
        
        var errors = validationFailures.Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .ToList();

        if (errors.Count > 0) throw new ValidationException(errors);
        
        return await next(cancellationToken);
    }
}
