using Ghanavats.Domain.Primitives.DomainEventMechanism;

namespace CleanArchitecture.Core.GameAggregate.Events;

public class GameSoftDeletedEvent : DomainNotificationMessageBase
{
    public int GameId { get; set; }

    public GameSoftDeletedEvent(int gameId)
    {
        GameId = gameId;
        NotificationMessage = "A game has been soft deleted.";
    }
}
