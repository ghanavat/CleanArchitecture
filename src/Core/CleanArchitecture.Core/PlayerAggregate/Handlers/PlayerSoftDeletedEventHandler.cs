using System.Diagnostics;
using CleanArchitecture.Core.PlayerAggregate.Events;
using MediatR;

namespace CleanArchitecture.Core.PlayerAggregate.Handlers;

public class PlayerSoftDeletedEventHandler : INotificationHandler<PlayerSoftDeletedEvent>
{
    public Task Handle(PlayerSoftDeletedEvent notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine(notification.NotificationMessage);
        Debug.WriteLine($"The player '{notification.PlayerId}' has just been soft deleted.");
        return Task.CompletedTask;
    }
}
