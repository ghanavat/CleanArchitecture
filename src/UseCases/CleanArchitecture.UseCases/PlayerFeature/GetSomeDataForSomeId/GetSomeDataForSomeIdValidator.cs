using FluentValidation;

namespace CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;

/// <summary>
/// Query validator
/// </summary>
public class GetSomeDataForSomeIdValidator : AbstractValidator<GetPlayerByIdQuery>
{
	// Suggesting to use 'Severity' for scenarios where validation issues should not prevent the operation to be completed

	/// <summary>
	/// Validator constructor where the validation rules defined.
	/// </summary>
	public GetSomeDataForSomeIdValidator()
	{
		RuleFor(x => x.PlayerId).NotEqual(0)
			.WithSeverity(Severity.Error);
	}
}
