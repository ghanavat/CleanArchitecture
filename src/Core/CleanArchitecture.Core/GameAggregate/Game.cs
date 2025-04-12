using CleanArchitecture.Core.GameAggregate.Events;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared.Attributes;
using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Shared.Extensions;
using Ghanavats.Domain.Primitives;

namespace CleanArchitecture.Core.GameAggregate;

/// <summary>
/// Game entity. This entity is internal and its identity referenced by the aggregate root
/// </summary>
public class Game : EntityBase, IAggregateRoot
{
    public string? PlayerId { get; set; }

    public string? Name { get; set; }

    public bool IsDeleted { get; set; }

    public string? Comment { get; set; }

    public DateOnly DateCreated { get; private set; } = DateOnly.FromDateTime(DateTime.Today);

    /// <summary>
    /// Default constructor
    /// </summary>
    public Game()
    {
    }

    /// <summary>
    /// Private constructor used only by the factory method
    /// </summary>
    /// <param name="name"></param>
    /// <param name="comment"></param>
    private Game(string name, string comment)
    {
        Name = name;
        Comment = comment;
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
    /// Softly deletes a game
    /// </summary>
    public void SoftDeleteGame()
    {
        IsDeleted = true;
    }

    /// <summary>
    /// Factory method to create the entire aggregate
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="gameName"></param>
    /// <param name="comment"></param>
    /// <returns></returns>
    [FactoryMethod(FactoryMethodFor.Game)]
    internal static Game AddNewGame(string playerId, string gameName, string comment)
    {
        var gameInstance = new Game(gameName, comment);
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
