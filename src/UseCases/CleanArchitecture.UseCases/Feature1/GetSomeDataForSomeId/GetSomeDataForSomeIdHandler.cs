using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Shared.Query;
using CleanArchitecture.Shared.ResultMechanism;
using CleanArchitecture.UseCases.Dtos;
using FluentValidation;

namespace CleanArchitecture.UseCases.Feature1.GetSomeDataForSomeId;

public class GetSomeDataForSomeIdHandler : IQueryHandler<GetSomeDataForSomeIdQuery, Result<SampleFilteredWithIdDto>>
{
    private readonly IRepository<SampleEntity> _repository;
    private readonly IValidator<GetSomeDataForSomeIdQuery> _validator;

    public GetSomeDataForSomeIdHandler(IRepository<SampleEntity> repository, IValidator<GetSomeDataForSomeIdQuery> validator)
    {
        _repository = repository.CheckForNull();
        _validator = validator.CheckForNull();
    }

    public async Task<Result<SampleFilteredWithIdDto>> Handle(GetSomeDataForSomeIdQuery request, CancellationToken cancellationToken)
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

        if (result == null) { return Result<SampleFilteredWithIdDto>.Error("Sample error message."); }

        return new SampleFilteredWithIdDto
        {
            Id = result.Id,
            Name = result.FirstName,
            Description = result.FirstName
        };
    }
}
