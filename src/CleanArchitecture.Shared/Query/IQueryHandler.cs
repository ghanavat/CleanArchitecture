using MediatR;

namespace CleanArchitecture.Shared.Query;

/// <summary>
/// Custom interface to handle CQRS queries request handlers as IRequestHandler
/// </summary>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
