using FluentValidation;

namespace CleanArchitecture.UseCases.Feature1.GetSomeDataForSomeId;

/// <summary>
/// Query validator
/// </summary>
public class GetSomeDataForSomeIdValidator : AbstractValidator<GetSomeDataForSomeIdQuery>
{
	// Suggesting to use 'Severity' for scenarios where validation issues should not prevent the operation to be completed

	public GetSomeDataForSomeIdValidator()
	{
		RuleFor(x => x.SomeId).NotEmpty()
			.WithSeverity(Severity.Error);
	}
}
