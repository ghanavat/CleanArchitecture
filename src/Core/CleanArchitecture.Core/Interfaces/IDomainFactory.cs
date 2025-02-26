using System.Runtime.InteropServices;
using CleanArchitecture.Core.ActionOptions;
using CleanArchitecture.Shared;

namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// Domain Factory interface. This is implemented in the Infrastructure layer.
/// </summary>
/// <remarks>
/// Inject this interface in the client code to access its member.
/// </remarks>
/// <typeparam name="TRequest">A mediatr (in this solution) command as IRequest/ICommand, although it does not have to be.</typeparam>
/// <typeparam name="TResponse">An aggregate root object.</typeparam>
public interface IDomainFactory<in TRequest, out TResponse>
    where TRequest : class
    where TResponse : EntityBase, IAggregateRoot
{
    /// <summary>
    /// Create domain entity object
    /// </summary>
    /// <param name="request">The request that is used to create an entity object with</param>
    /// <param name="action">Optional action to enforce further behaviour or options</param>
    /// <returns>An instance of <typeparamref name="TResponse"/></returns>
    TResponse? CreateEntityObject(TRequest request, [Optional] Action<DomainFactoryOption> action);
}
