namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// This is a marker interface. You can use this to mark your root entities.
/// By marking your root entities, you can apply constraint to your Repository, so domain operations are done only on root entities.
/// </summary>
public interface IAggregateRoot { }
