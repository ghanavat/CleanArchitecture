namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// This is a marker interface which can be applied to aggregate root entities.
/// Then you can apply constraints to your repositories, so the repository implementation can only be applied on root entities (aggregate roots).
/// </summary>
public interface IAggregateRoot { }
