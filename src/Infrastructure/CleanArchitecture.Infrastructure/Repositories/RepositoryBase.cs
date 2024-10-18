using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Extensions;
using System.Linq.Expressions;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.Repositories;

/// <inheritdoc/>
public abstract class RepositoryBase<T> : IRepository<T> 
    where T : EntityBase, IAggregateRoot
{
    private readonly PlayGroundDbContext _efDbContext;

    /// <summary>
    /// Repository Base constructor
    /// </summary>
    /// <param name="efDbContext">EF DB Context dependency</param>
    protected RepositoryBase(PlayGroundDbContext efDbContext)
    {
        _efDbContext = efDbContext.CheckForNull();
    }

    /// <inheritdoc/>
    public virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : notnull
    {
        var result = await _efDbContext.Set<T>().FindAsync([id], cancellationToken);
        return result;
    }

    /// <inheritdoc/>
    public virtual Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Do not use. This is a marker class to allow IRepository to be registered in composition root.
/// </summary>
/// <typeparam name="T">An entity to which the repository operations will be implemented for</typeparam>
public class MarkerRepository<T> : RepositoryBase<T> where T : EntityBase, IAggregateRoot
{
    public MarkerRepository(PlayGroundDbContext efContext) 
        : base(efContext)
    { }
}
