namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// A base abstraction for mutable operations
/// <see>
///     <cref>https://deviq.com/design-patterns/repository-pattern</cref>
/// </see>
/// </summary>
/// <typeparam name="T">An entity to which the repository operations will be written for.</typeparam>
public interface IRepository<T> : IReadRepository<T> where T : class
{
    /// <summary>
    /// To persist/add a new entity to the DB
    /// </summary>
    /// <param name="entity">The entity that it to be added</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// The task result of the <typeparamref name="T" />.
    /// </returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// To update an entity
    /// </summary>
    /// <param name="entity">The entity to be updated</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// The task result />
    /// </returns>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// To delete an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// The task result
    /// </returns>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
