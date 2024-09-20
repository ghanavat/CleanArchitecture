using CleanArchitecture.Shared.Command;
using CleanArchitecture.Shared.ResultMechanism;

namespace CleanArchitecture.UseCases.Feature1.Create;

public record CreateSampleCommand(string FirstName, string LastName) : ICommand<Result<bool>>;