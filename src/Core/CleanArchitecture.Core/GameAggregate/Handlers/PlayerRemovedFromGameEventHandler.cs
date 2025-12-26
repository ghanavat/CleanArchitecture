using CleanArchitecture.Core.GameAggregate.Events;
using MediatR;
using System.Diagnostics;

namespace CleanArchitecture.Core.GameAggregate.Handlers;

/// <summary>
/// This is the handler of an event, in this template, SampleFinishedEvent. 
/// We implement what we want to do upon triggering the event; e.g. Sending an email, or use any other third party notification mechanisms.
/// </summary>
public class PlayerRemovedFromGameEventHandler : INotificationHandler<PlayerRemovedFromGameEvent>
{
    public Task Handle(PlayerRemovedFromGameEvent notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine(notification.PlayerId);
        Debug.WriteLine($"The player '{notification.PlayerId}' has just been removed from the game {notification.GameId}");
        return Task.CompletedTask;
    }
}
