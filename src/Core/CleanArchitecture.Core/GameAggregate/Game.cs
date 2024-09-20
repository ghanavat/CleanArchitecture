using CleanArchitecture.Shared.Extensions;

namespace CleanArchitecture.Core.GameAggregate;

/// <summary>
/// Game entity. This entity is internal and its identity referenced by the aggregate root
/// </summary>
public class Game : EntityBase
{
    public string? PlayerId { get; set; }
    public string? GameName { get; set; }

    /// <summary>
    /// Add a new player to a game object
    /// </summary>
    /// <param name="playerId">Player id that is going to be added</param>
    /// <returns>Void</returns>
    public void AddPlayer(string playerId)
    {
        PlayerId = playerId.CheckNotNull();
        // TODO publish domain event here
    }

    /// <summary>
    /// Remove player from game object
    /// </summary>
    /// <param name="playerId">Player id that is going to be removed</param>
    public void RemovePlayer(string playerId)
    {
        PlayerId = null;
        //TODO Publish domain event here
    }
}
