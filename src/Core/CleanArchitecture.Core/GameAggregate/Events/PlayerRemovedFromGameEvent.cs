using Ghanavats.Domain.Primitives.DomainEventMechanism;

namespace CleanArchitecture.Core.GameAggregate.Events;

public class PlayerRemovedFromGameEvent : DomainNotificationMessageBase
{
    public int PlayerId { get; set; }
    public int GameId { get; set; }

    public PlayerRemovedFromGameEvent(int playerId, int gameId)
    {
        PlayerId = playerId;
        GameId = gameId;
        NotificationMessage = "Player has been removed from the game";
    }
}
