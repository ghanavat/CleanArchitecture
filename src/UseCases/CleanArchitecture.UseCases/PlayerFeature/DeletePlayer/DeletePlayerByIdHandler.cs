using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Shared.Command;
using CleanArchitecture.Shared.Extensions;
using FluentValidation;
using Ghanavats.Repository.Abstractions;
using Ghanavats.ResultPattern;
using Ghanavats.ResultPattern.Enums;

namespace CleanArchitecture.UseCases.PlayerFeature.DeletePlayer;

public class DeletePlayerByIdHandler : ICommandHandler<DeletePlayerByIdCommand, Result<bool>>
{
    private readonly IRepository<Player> _repository;
    private readonly IValidator<DeletePlayerByIdCommand> _validator;
    
    public DeletePlayerByIdHandler(IRepository<Player> repository, 
        IValidator<DeletePlayerByIdCommand> validator)
    {
        _repository = repository.CheckForNull();
        _validator = validator.CheckForNull();
    }
    
    public async Task<Result<bool>> Handle(DeletePlayerByIdCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult);
        }

        var player = await _repository.GetByIdAsync(request.PlayerId, cancellationToken);
        if (player is null)
        {
            return Result.NotFound();
        }

        player.SoftDeletePlayer();
        var softDeleteResult = await _repository.UpdateAsync(player, cancellationToken);
        if (!softDeleteResult.IsDeleted)
        {
            return Result.Error("Failed to delete player", ErrorKind.BusinessRule);
        }
        
        return true;
    }
}
