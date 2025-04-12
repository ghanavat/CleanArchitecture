using Ghanavats.Domain.Primitives.DomainEventMechanism;

namespace CleanArchitecture.Core.GameAggregate.Events;

/// <summary>
/// An event is something that happens before you want the other subdomain to become aware or it. More like a messaging events.
/// An event in the Core/Domain layer is dispatched when something changes. For instance, the content of an event can be a date/time on which the changes occurred.
/// It can be used to trigger the MediatR Notification mechanism. This mechanism can be implemented as Email, Queue Message, SMS or Log.
/// </summary>
public class NewPlayerAddedToGameEvent : DomainNotificationMessageBase
{
    public Game Game { get; set; }
    public string PlayerId { get; set; }

    public NewPlayerAddedToGameEvent(Game game, string playerId)
    {
        Game = game;
        PlayerId = playerId;
    }
}
