using CleanArchitecture.Shared;

namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// Domain Factory interface. This is implemented in Infrastructure layer.
/// </summary>
/// <remarks>
/// Inject this interface in the client code to access its member.
/// </remarks>
/// <typeparam name="TRequest">A mediatr command as IRequest</typeparam>
/// <typeparam name="TResponse">An aggregate root object.</typeparam>
public interface IDomainFactory<in TRequest, out TResponse>
    where TRequest : class
    where TResponse : EntityBase, IAggregateRoot
{
    /// <summary>
    /// Domain factory member in which the aggregate root get created.
    /// </summary>
    /// <param name="instance"></param>
    /// <returns>An instance of <typeparamref name="TResponse"/></returns>
    TResponse? CreateEntityObject(TRequest instance);
}
