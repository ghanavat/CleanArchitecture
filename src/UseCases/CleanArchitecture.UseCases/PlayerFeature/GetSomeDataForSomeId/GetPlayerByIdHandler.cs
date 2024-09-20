using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Shared.Query;
using CleanArchitecture.Shared.ResultMechanism;
using CleanArchitecture.UseCases.Dtos;
using FluentValidation;

namespace CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;

public class GetPlayerByIdHandler : IQueryHandler<GetPlayerByIdQuery, Result<FilteredPlayerDto>>
{
    private readonly IRepository<Player> _repository;
    private readonly IValidator<GetPlayerByIdQuery> _validator;

    public GetPlayerByIdHandler(IRepository<Player> repository, IValidator<GetPlayerByIdQuery> validator)
    {
        _repository = repository.CheckForNull();
        _validator = validator.CheckForNull();
    }

    public async Task<Result<FilteredPlayerDto>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
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
        
        var result = await _repository.GetByIdAsync(request.SomeId, cancellationToken);

        if (result == null) { return Result<FilteredPlayerDto>.Error("Sample error message."); }

        return new FilteredPlayerDto
        {
            Id = result.Id,
            Name = $"{result.FirstName} {result.LastName}"
        };
    }
}
