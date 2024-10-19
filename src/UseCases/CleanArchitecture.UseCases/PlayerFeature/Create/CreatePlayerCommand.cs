using CleanArchitecture.Shared.Command;
using CleanArchitecture.Shared.ResultMechanism;

namespace CleanArchitecture.UseCases.PlayerFeature.Create;

/// <summary>
/// Create Player aggregate command
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Comment"></param>
public record CreatePlayerCommand(string FirstName, string LastName, string Comment) : ICommand<Result<int>>;
