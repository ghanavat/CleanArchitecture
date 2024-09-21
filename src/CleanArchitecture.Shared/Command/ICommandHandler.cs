using MediatR;

namespace CleanArchitecture.Shared.Command;

/// <summary>
/// Custom interface to handle CQRS commands request handlers as IRequestHandler
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}
