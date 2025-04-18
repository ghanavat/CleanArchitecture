﻿using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Shared.Query;
using CleanArchitecture.UseCases.Dtos;
using FluentValidation;
using Ghanavats.Repository.Abstractions;
using Ghanavats.ResultPattern;
using Ghanavats.ResultPattern.Enums;

namespace CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;

/// <summary>
/// Get Player by ID handler
/// </summary>
public class GetPlayerByIdHandler : IQueryHandler<GetPlayerByIdQuery, Result<FilteredPlayerDto>>
{
    private readonly IRepository<Player> _repository;
    private readonly IValidator<GetPlayerByIdQuery> _validator;

    /// <summary>
    /// Handler constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="validator"></param>
    public GetPlayerByIdHandler(IRepository<Player> repository, IValidator<GetPlayerByIdQuery> validator)
    {
        _repository = repository.CheckForNull();
        _validator = validator.CheckForNull();
    }

    /// <inheritdoc/>
    public async Task<Result<FilteredPlayerDto>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        /* TODO: This can be an extension method */
        var errors = validationResult.Errors.Select(validationError => new ValidationError
        {
            ErrorCode = validationError.ErrorCode,
            ErrorMessage = validationError.ErrorMessage,
            ValidationErrorType = (ValidationErrorType)validationError.Severity
        });

        if (!validationResult.IsValid)
        {
            return Result.Invalid(errors);
        }

        var result = await _repository.GetByIdAsync(request.PlayerId, cancellationToken);

        if (result == null)
        {
            return Result<FilteredPlayerDto>.Error("Sample error message.");
        }

        return new FilteredPlayerDto
        {
            Id = result.Id,
            FullName = $"{result.FirstName} {result.LastName}",
            IsDeleted = result.IsDeleted
        };
    }
}
