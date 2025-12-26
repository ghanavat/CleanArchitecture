using CleanArchitecture.Core.GameAggregate.Events;
using Ghanavats.Domain.Factory.Attributes;
using Ghanavats.Domain.Primitives;
using Ghanavats.Domain.Primitives.Attributes;

namespace CleanArchitecture.Core.GameAggregate;

/// <summary>
/// Game entity. This entity is internal and its identity referenced by the aggregate root
/// </summary>
[AggregateRoot(nameof(Game))]
public class Game : EntityBase
{
    public int? PlayerId { get; set; }

    public string? Name { get; init; }

    public bool IsDeleted { get; set; }

    public string? Comment { get; init; }

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
    /// <param name="playerId"></param>
    /// <param name="name"></param>
    /// <param name="comment"></param>
    private Game(int playerId, string name, string comment)
    {
        Name = name;
        Comment = comment;
        PlayerId = playerId;
    }

    /// <summary>
    /// Factory method to create the entire aggregate
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="gameName"></param>
    /// <param name="comment"></param>
    /// <returns>Fully populated Game object</returns>
    [FactoryMethod]
    internal static Game AddNewGame(int playerId, string gameName, string comment)
    {
        var gameInstance = new Game(playerId, gameName, comment);
        
        var newGameEvent = new NewGameCreatedEvent(gameInstance);
        gameInstance.AddDomainEvent(newGameEvent);
        
        return gameInstance;
    }

    /// <summary>
    /// Removes player from the game object
    /// </summary>
    /// <param name="playerId">Player id that is going to be removed</param>
    public void RemovePlayer(int playerId)
    {
        PlayerId = null;
        
        var playerRemovedFromGameEvent = new PlayerRemovedFromGameEvent(playerId, Id);
        AddDomainEvent(playerRemovedFromGameEvent);
    }

    /// <summary>
    /// Softly deletes a game
    /// </summary>
    public void SoftDeleteGame()
    {
        IsDeleted = true;
        
        var gameSoftDeleteEvent = new GameSoftDeletedEvent(Id);
        AddDomainEvent(gameSoftDeleteEvent);
    }
}
