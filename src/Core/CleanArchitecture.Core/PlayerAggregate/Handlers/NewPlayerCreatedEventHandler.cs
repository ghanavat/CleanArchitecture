using System.Diagnostics;
using CleanArchitecture.Core.PlayerAggregate.Events;
using MediatR;

namespace CleanArchitecture.Core.PlayerAggregate.Handlers;

public class NewPlayerCreatedEventHandler : INotificationHandler<NewPlayerCreatedEvent>
{
    public Task Handle(NewPlayerCreatedEvent notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine(notification.NotificationMessage);
        Debug.WriteLine($"A new player with the Id '{notification.Player.Id}' has just been created.");
        return Task.CompletedTask;
    }
}
