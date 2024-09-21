using MediatR;

namespace CleanArchitecture.Shared.Query;

/// <summary>
/// Custom interface to handle CQRS queries as IRequest and responses
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
