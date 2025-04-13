namespace CleanArchitecture.Shared.Enums;

/// <summary>
/// An enum with name of aggregate roots to allow the factory methods be found and invoked.
/// Values must match the name of aggregate roots
/// </summary>
public enum FactoryMethodFor
{
    None,
    Game,
    Player
}
