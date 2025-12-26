using CleanArchitecture.Core.GameAggregate.Events;
using MediatR;
using System.Diagnostics;

namespace CleanArchitecture.Core.GameAggregate.Handlers;

public class GameSoftDeletedEventHandler : INotificationHandler<GameSoftDeletedEvent>
{
    public Task Handle(GameSoftDeletedEvent notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine(notification.NotificationMessage);
        Debug.WriteLine($"The game '{notification.GameId}' has just been soft deleted.");
        return Task.CompletedTask;
    }
}
