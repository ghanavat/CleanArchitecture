﻿using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Shared.Command;
using CleanArchitecture.Shared.Extensions;
using FluentValidation;
using Ghanavats.Domain.Factory.Abstractions;
using Ghanavats.Repository.Abstractions;
using Ghanavats.ResultPattern;
using Ghanavats.ResultPattern.Enums;

namespace CleanArchitecture.UseCases.PlayerFeature.Create;

/// <summary>
/// Create Player handler implementation
/// </summary>
public class CreatePlayerHandler : ICommandHandler<CreatePlayerCommand, Result<int>>
{
    private readonly IRepository<Player> _repository;
    private readonly IValidator<CreatePlayerCommand> _validator;
    private readonly IDomainFactory<CreatePlayerCommand, Player> _domainFactory;

    /// <summary>
    /// Handler constructor
    /// </summary>
    /// <param name="repository">The repository dependency, used for persisting the data.</param>
    /// <param name="validator">Validator dependency, used to perform async validation before repository operation</param>
    /// <param name="domainFactory">Domain factory dependency, used to create the entity object for the repository.</param>
    public CreatePlayerHandler(IRepository<Player> repository, 
        IValidator<CreatePlayerCommand> validator,
        IDomainFactory<CreatePlayerCommand, Player> domainFactory)
    {
        _repository = repository.CheckForNull();
        _validator = validator.CheckForNull();
        _domainFactory = domainFactory.CheckForNull();
    }
    
    /// <inheritdoc/>
    public async Task<Result<int>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        /* TODO: This can be an extension method */
        var errors = validationResult.Errors.Select(validationError => new ValidationError
        {
            ErrorCode = validationError.ErrorCode,
            ErrorMessage = validationError.ErrorMessage,
            ValidationErrorType = (ValidationErrorType)validationError.Severity
        });

        if (!validationResult.IsValid) return Result.Invalid(errors);
        
        var domainFactoryResponseModel = _domainFactory.CreateEntityObject(request);
        if (domainFactoryResponseModel.Value is null)
        {
            return Result<int>.Error($"Something has gone wrong and we were unable to create new object for {nameof(Player)} entity");
        }
        
        var newPlayerResult = await _repository.AddAsync(domainFactoryResponseModel.Value, cancellationToken);
        if (newPlayerResult.Id.Equals(0))
        {
            return Result<int>.Error($"Something has gone wrong and we were unable to persist the data for {nameof(Player)}");
        }
        
        return newPlayerResult.Id;
    }
}
