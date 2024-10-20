using FluentValidation;

namespace CleanArchitecture.UseCases.PlayerFeature.Create;

/// <summary>
/// Create Player command validator
/// </summary>
public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    /// <summary>
    /// Validator constructor where the validation rules defined.
    /// </summary>
    public CreatePlayerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithSeverity(Severity.Error);
    }
}
