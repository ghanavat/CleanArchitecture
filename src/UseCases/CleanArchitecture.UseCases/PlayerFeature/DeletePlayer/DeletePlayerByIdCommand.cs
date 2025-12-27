using CleanArchitecture.Shared.Command;
using Ghanavats.ResultPattern;

namespace CleanArchitecture.UseCases.PlayerFeature.DeletePlayer;

public record DeletePlayerByIdCommand(int PlayerId) : ICommand<Result<bool>>;
