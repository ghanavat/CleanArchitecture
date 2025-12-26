using Ghanavats.Domain.Primitives.DomainEventMechanism;

namespace CleanArchitecture.Core.PlayerAggregate.Events;

public class PlayerSoftDeletedEvent : DomainNotificationMessageBase
{
    public int PlayerId { get; set; }

    public PlayerSoftDeletedEvent(int playerId)
    {
        PlayerId = playerId;
        NotificationMessage = "A player has been soft deleted.";
    }
}
