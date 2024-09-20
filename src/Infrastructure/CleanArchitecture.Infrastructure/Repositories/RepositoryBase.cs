using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using System.Text;
using CleanArchitecture.Core;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.Repositories;

/// <inheritdoc/>
public abstract class RepositoryBase<T> : IRepository<T> where T : class, IAggregateRoot
{
    //private readonly IMongoClient _client;
    //private readonly IMongoDatabase _dbContext;
    private readonly SampleDbContext _efDbContext;
    //private readonly string _collectionName;

    /// <inheritdoc/>
    protected RepositoryBase(SampleDbContext efDbContext) //string collectionName
    {
        // Inject your DbContext here

        //_dbContext = dbContext.CheckNotNull(); //_client.GetDatabase("employer");
        //_collectionName = collectionName.CheckNotNull();
        _efDbContext = efDbContext.CheckNotNull();
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _efDbContext.Set<T>().Add(entity);
        await _efDbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

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

    public virtual Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /* Only needed when MongoDb Driver is used.
     * Idea is to prevent repeating a call to 'GetCollection'.
     */
    //protected IAggregateFluent<T> BaseAggregate()
    //{
    //    var sampleEntityCollection = _dbContext.GetCollection<T>(string.Empty);
    //    return sampleEntityCollection.Aggregate();
    //}
}
