using CleanArchitecture.Core.GameAggregate.Events;
using MediatR;
using System.Diagnostics;

namespace CleanArchitecture.Core.GameAggregate.Handlers;

/// <summary>
/// This is the handler of an event, in this template, SampleFinishedEvent. 
/// We implement what we want to do upon triggering the event; e.g. Sending an email, or use any other third party notification mechanisms.
/// </summary>
public class SampleFinishedEventHandler : INotificationHandler<NewPlayerAddedToGameEvent>
{
    public Task Handle(NewPlayerAddedToGameEvent notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Something happened and an event was dispatched.");
        Debug.WriteLine($"The game '{notification.Game.Name}' has just been created with the associated player {notification.PlayerId}");

        return Task.CompletedTask;
    }
}
