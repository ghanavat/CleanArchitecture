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
    /// <summary>
    /// First Name domain property
    /// </summary>
    public string? FirstName { get; private set; }

    /// <summary>
    /// Last Name domain property
    /// </summary>
    public string? LastName { get; private set; }

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
    private Player(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Factory method to create the entire aggregate
    /// </summary>
    [FactoryMethod(FactoryMethodFor.Player)]
    internal static Player AddPlayer(string firstName, string lastName)
    {
        return new Player(firstName, lastName);
    }

    /// <summary>
    /// Update player details
    /// </summary>
    /// <param name="firstName">firstName of the player</param>
    /// <param name="lastName">lastName of the player</param>
    public void UpdatePlayerDetails(string firstName, string lastName)
    {
        FirstName = firstName.CheckForNull();
        LastName = lastName.CheckForNull();
    }
}
