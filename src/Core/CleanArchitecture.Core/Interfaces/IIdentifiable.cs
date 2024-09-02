namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// Identity interface to be used by all entities in which a PK is needed
/// </summary>
public interface IIdentifiable
{
    string Id { get; }
}
