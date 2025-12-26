using CleanArchitecture.Core.GameAggregate.Events;
using MediatR;
using System.Diagnostics;

namespace CleanArchitecture.Core.GameAggregate.Handlers;

public class NewGameCreatedEventHandler : INotificationHandler<NewGameCreatedEvent>
{
    public Task Handle(NewGameCreatedEvent notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine(notification.NotificationMessage);
        Debug.WriteLine($"The game '{notification.Game.Name}' has just been created with the associated player {notification.Game.PlayerId}");
        return Task.CompletedTask;
    }
}
