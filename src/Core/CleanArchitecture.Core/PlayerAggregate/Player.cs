using CleanArchitecture.Shared.Extensions;
using System.Runtime.CompilerServices;
using CleanArchitecture.Core.PlayerAggregate.Events;
using Ghanavats.Domain.Factory.Attributes;
using Ghanavats.Domain.Primitives;
using Ghanavats.Domain.Primitives.Attributes;

[assembly: InternalsVisibleTo("UseCases.Tests")]

namespace CleanArchitecture.Core.PlayerAggregate;

/// <summary>
/// Player entity.
/// This is the aggregate root.
/// </summary>
[AggregateRoot(nameof(Player))]
public class Player : EntityBase
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public bool IsDeleted { get; private set; }
    public string? Comment { get; private set; }
    public DateOnly DateCreated { get; private set; } = DateOnly.FromDateTime(DateTime.Today);
    
    /// <summary>
    /// Default constructor
    /// </summary>
    public Player()
    {
    }

    /// <summary>
    /// Private constructor used only by the factory method
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="comment"></param>
    private Player(string firstName, string lastName, string comment)
    {
        FirstName = firstName;
        LastName = lastName;
        Comment = comment;
    }

    /// <summary>
    /// Factory method to create the entire aggregate
    /// </summary>
    [FactoryMethod("Player")]
    internal static Player AddPlayer(string firstName, string lastName, string comment)
    {
        var player = new Player(firstName, lastName, comment);
        
        var newPlayerCreatedEvent  = new NewPlayerCreatedEvent(player);
        player.AddDomainEvent(newPlayerCreatedEvent);
        
        return player;
    }

    /// <summary>
    /// Update player details
    /// </summary>
    /// <param name="firstName">firstName of the player</param>
    /// <param name="lastName">lastName of the player</param>
    /// <param name="comment"></param>
    public void UpdatePlayerDetails(string firstName, string lastName, string comment)
    {
        FirstName = firstName.CheckForNull();
        LastName = lastName.CheckForNull();
        Comment = comment;
    }

    /// <summary>
    /// Softly deletes a game
    /// </summary>
    public void SoftDeletePlayer()
    {
        IsDeleted = true;
        
        var playerSoftDeletedEvent = new PlayerSoftDeletedEvent(Id);
        AddDomainEvent(playerSoftDeletedEvent);
    }
}
