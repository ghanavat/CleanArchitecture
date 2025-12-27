using FluentValidation;

namespace CleanArchitecture.UseCases.PlayerFeature.DeletePlayer;

public class DeletePlayerByIdCommandValidator : AbstractValidator<DeletePlayerByIdCommand>
{
    public DeletePlayerByIdCommandValidator()
    {
        RuleFor(command => command.PlayerId).NotEmpty();
    }
}
