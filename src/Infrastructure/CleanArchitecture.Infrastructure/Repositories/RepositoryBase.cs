using CleanArchitecture.Infrastructure.Data;
using Ghanavats.Domain.Primitives;
using Ghanavats.Domain.Primitives.Attributes;
using Ghanavats.Repository;

namespace CleanArchitecture.Infrastructure.Repositories;

/// <summary>
/// Do not use. This is a marker class to allow IRepository to be registered in composition root.
/// </summary>
/// <typeparam name="T">An entity to which the repository operations will be implemented for</typeparam>
public class MarkerRepository<T> : RepositoryBase<T> where T : EntityBase
{
    /// <summary>
    /// Runs once - ever - per type T
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    static MarkerRepository()
    {
        var hasAttribute = typeof(T).GetCustomAttributes(typeof(AggregateRootAttribute), inherit: false).Length != 0;
        if (!hasAttribute)
        {
            throw new InvalidOperationException(
                $"The type {typeof(T).Name} must be marked with [AggregateRoot] to be used in this repository.");
        }
    }
    public MarkerRepository(PlayGroundDbContext efContext) 
        : base(efContext)
    { }
}
