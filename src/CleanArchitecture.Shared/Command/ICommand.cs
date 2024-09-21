using MediatR;

namespace CleanArchitecture.Shared.Command;

/// <summary>
/// Custom interface to handle CQRS commands as IRequest and responses
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
