using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Shared.DomainEventMechanism;

/// <summary>
/// Base notification message.
/// </summary>
public abstract class DomainNotifictionMessageBase : INotification
{
    [NotMapped]
    public string? NotificationMessage { get; protected set; } = "Something significant has just happened. Pass it on.";
}
