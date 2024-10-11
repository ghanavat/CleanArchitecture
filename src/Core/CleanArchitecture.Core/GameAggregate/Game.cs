using CleanArchitecture.Core.GameAggregate.Events;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Attributes;
using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Shared.Extensions;

namespace CleanArchitecture.Core.GameAggregate;

/// <summary>
/// Game entity. This entity is internal and its identity referenced by the aggregate root
/// </summary>
public class Game : EntityBase, IAggregateRoot
{
    /// <summary>
    /// Player ID domain property
    /// </summary>
    public string? PlayerId { get; set; }

    /// <summary>
    /// Game Name domain property
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Game()
    {
    }

    /// <summary>
    /// Private constructor used only by the factory method
    /// </summary>
    /// <param name="gameName"></param>
    private Game(string gameName)
    {
        Name = gameName;
    }

    /// <summary>
    /// Remove player from the game object
    /// </summary>
    /// <param name="playerId">Player id that is going to be removed</param>
    public void RemovePlayer(string playerId)
    {
        PlayerId = null;
    }
    
    /// <summary>
    /// Factory method to create the entire aggregate
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="gameName"></param>
    /// <returns></returns>
    [FactoryMethod(FactoryMethodFor.Game)]
    internal static Game AddNewGame(string playerId, string gameName)
    {
        var gameInstance = new Game(gameName);
        gameInstance.AddPlayer(playerId);

        return gameInstance;
    }
    
    /// <summary>
    /// Add a new player to the game object
    /// </summary>
    /// <param name="playerId">Player id that is going to be added</param>
    /// <returns>Void</returns>
    private void AddPlayer(string playerId)
    {
        PlayerId = playerId.CheckForNull();
        
        var newPlayerEvent = new NewPlayerAddedToGameEvent(this, playerId);
        AddDomainEvent(newPlayerEvent);
    }
}
