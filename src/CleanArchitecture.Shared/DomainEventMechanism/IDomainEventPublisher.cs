using CleanArchitecture.Shared.DomainEventMechanism;

namespace CleanArchitecture.Shared.DomainEventMechanism;

/// <summary>
/// An interface for publishing events
/// </summary>
public interface IDomainEventPublisher
{
    /// <summary>
    /// Publish events
    /// </summary>
    /// <param name="domainEvents">List of events</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishDomainEventsAsync(IEnumerable<DomainNotifictionMessageBase> domainEvents, CancellationToken cancellationToken);
}
