using System.Linq.Expressions;
using CleanArchitecture.Shared;

namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// A base abstraction for read-only operations.
/// Don't use this interface directly for custom repositories.
/// </summary>
/// <typeparam name="T">An entity to which the repository operations will be written against</typeparam>
public interface IReadRepository<T> where T : EntityBase, IAggregateRoot
{
    /// <summary>
    /// Finds an entity with the given id (typically is a primary key.
    /// </summary>
    /// <typeparam name="TId">The type of id.</typeparam>
    /// <param name="id">The value of the id.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="T" />, or <see langword="null"/>.
    /// </returns>
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

    /// <summary>
    /// Finds all entities of <typeparamref name="T" /> from the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<T>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all entities of <typeparamref name="T" />, that matches the predicate of the
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
