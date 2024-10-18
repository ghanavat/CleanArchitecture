using CleanArchitecture.Shared.DomainEventMechanism;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Shared;

/// <summary>
/// Abstract entity base.
/// </summary>
public abstract class EntityBase : DomainNotifictionMessageBase
{
    private readonly List<DomainNotifictionMessageBase> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<DomainNotifictionMessageBase> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// We are assuming that the ID type is string. ObjectId in MongoDb and string in DotNet.
    /// </summary>
    public int Id { get; set; }

    protected EntityBase()
    { }

    protected EntityBase(int id)
    {
        Id = id;
    }

    protected void AddDomainEvent(DomainNotifictionMessageBase notificationMessages)
    {
        _domainEvents.Add(notificationMessages);
    }

    public void RemoveDomainEvent(DomainNotifictionMessageBase eventItem)
    {
        _domainEvents.Remove(eventItem);
    }
}
