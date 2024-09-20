using CleanArchitecture.Shared.Command;
using CleanArchitecture.Shared.ResultMechanism;

namespace CleanArchitecture.UseCases.PlayerFeature.Create;

public record CreatePlayerCommand(string FirstName, string LastName) : ICommand<Result<string>>;
