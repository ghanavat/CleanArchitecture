namespace CleanArchitecture.Core.Aggregates.Events;

/// <summary>
/// An event is something that happens before you want the other subdomain to become aware or it. More like a messaging events.
/// An event in the Core/Domain layer is dispatched when something changes. For instance, the content of an event can be a date/time on which the changes accurred.
/// It can be used to trigger the MediatR Notification mechanism. This mechanism can be implemented as Email, Queue Message, SMS or Log.
/// </summary>
internal class SampleFinishedEvent
{
}
