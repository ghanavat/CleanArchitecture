using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Attributes;
using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Shared.Extensions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UseCases.Tests")]

namespace CleanArchitecture.Core.PlayerAggregate;

/// <summary>
/// Player entity.
/// This is the aggregate root.
/// </summary>
public class Player : EntityBase, IAggregateRoot
{
    public string FirstName { get; private set; } = string.Empty;
    
    public string? LastName { get; private set; }

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
    [FactoryMethod(FactoryMethodFor.Player)]
    internal static Player AddPlayer(string firstName, string lastName, string comment)
    {
        return new Player(firstName, lastName, comment);
    }
}
