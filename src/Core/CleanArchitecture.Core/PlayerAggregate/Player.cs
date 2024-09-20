using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Extensions;

namespace CleanArchitecture.Core.PlayerAggregate;

/// <summary>
/// Player entity.
/// This is the aggregate root.
/// </summary>
public class Player : EntityBase, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    
    public Player(string firstName, string lastName)
    {
        FirstName = firstName.CheckForNull();
        LastName = lastName.CheckForNull();
    }

    /// <summary>
    /// Big no. Bad idea!!!
    /// </summary>
    public void AddPlayer()
    {
        /* 
         * For documentation/intention purposes: 
         * This is not supposed to be here. 
         * When an entity needs to be populated for the first time, it is not a domain operation. 
         * Instead, it needs to be dealt with at the application layer.
        */
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
