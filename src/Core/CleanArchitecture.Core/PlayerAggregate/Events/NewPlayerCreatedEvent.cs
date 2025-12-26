using Ghanavats.Domain.Primitives.DomainEventMechanism;

namespace CleanArchitecture.Core.PlayerAggregate.Events;

public class NewPlayerCreatedEvent : DomainNotificationMessageBase
{
    public Player Player { get; set; }

    public NewPlayerCreatedEvent(Player player)
    {
        Player = player;
        NotificationMessage = "A player has been created";
    }
}
