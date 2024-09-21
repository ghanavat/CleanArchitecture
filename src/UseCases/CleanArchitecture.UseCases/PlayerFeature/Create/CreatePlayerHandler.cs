using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Shared.Command;
using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Shared.ResultMechanism;
using FluentValidation;

namespace CleanArchitecture.UseCases.PlayerFeature.Create;

/// <summary>
/// Create Player handler implementation
/// </summary>
public class CreatePlayerHandler : ICommandHandler<CreatePlayerCommand, Result<string>>
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
    public async Task<Result<string>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        /* This can be an extension method */
        var errors = validationResult.Errors.Select(validationError => new ValidationError
        {
            ErrorCode = validationError.ErrorCode,
            ErrorMessage = validationError.ErrorMessage,
            ValidationErrorType = (ValidationErrorType)validationError.Severity
        });

        if (!validationResult.IsValid) return Result.Invalid(errors);

        var player = _domainFactory.CreateEntityObject(request);
        if (player is null)
        {
            return Result<string>.Error($"Something has gone wrong and we were unable to create new object for {nameof(Player)} entity");
        }
        
        var newPlayerResult = await _repository.AddAsync(player, cancellationToken);
        if (newPlayerResult.Id == null)
        {
            return Result<string>.Error($"Something has gone wrong and we were unable to persist the data for {nameof(Player)}");
        }
        
        return newPlayerResult.Id;
    }
}
