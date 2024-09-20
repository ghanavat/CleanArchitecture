using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Shared.Command;
using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Shared.ResultMechanism;
using FluentValidation;

namespace CleanArchitecture.UseCases.PlayerFeature.Create;

public class CreatePlayerHandler : ICommandHandler<CreatePlayerCommand, Result<string>>
{
    private readonly IRepository<Player> _repository;
    private readonly IValidator<CreatePlayerCommand> _validator;

    public CreatePlayerHandler(IRepository<Player> repository, IValidator<CreatePlayerCommand> validator)
    {
        _repository = repository.CheckNotNull();
        _validator = validator.CheckNotNull();
    }
    
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

        var newPlayer = new Player(request.FirstName, request.LastName);
        var createPlayerResult = await _repository.AddAsync(newPlayer, cancellationToken);

        if (createPlayerResult.Id == null) return Result<string>.Error("Unable to create new player.");
        return createPlayerResult.Id;
    }
}
