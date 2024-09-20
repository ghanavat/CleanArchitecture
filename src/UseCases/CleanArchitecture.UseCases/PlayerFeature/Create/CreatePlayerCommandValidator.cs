using FluentValidation;

namespace CleanArchitecture.UseCases.PlayerFeature.Create;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithSeverity(Severity.Error);
        RuleFor(x => x.LastName).NotEmpty().WithSeverity(Severity.Error);
    }
}
