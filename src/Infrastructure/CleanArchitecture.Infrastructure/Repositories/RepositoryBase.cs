﻿using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Extensions;
using System.Linq.Expressions;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.Repositories;

/// <inheritdoc/>
public abstract class RepositoryBase<T> : IRepository<T> 
    where T : EntityBase, IAggregateRoot
{
    //private readonly IMongoClient _client;
    //private readonly IMongoDatabase _dbContext;
    private readonly SampleDbContext _efDbContext;
    //private readonly string _collectionName;

    /// <summary>
    /// Repository Base constructor
    /// </summary>
    /// <param name="efDbContext">EF DB Context dependency</param>
    protected RepositoryBase(SampleDbContext efDbContext) //string collectionName
    {
        // Inject your DbContext here

        //_dbContext = dbContext.CheckNotNull(); //_client.GetDatabase("databaseName");
        //_collectionName = collectionName.CheckNotNull();
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
        /*Using MongoDb Driver*/
        //var aggregate = BaseAggregate();
        //var match = aggregate.Match(x => x.Id == id);

        //return await match.FirstOrDefaultAsync(cancellationToken);

        /*Using MongoDb EF Core*/
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
