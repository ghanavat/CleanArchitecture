using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Infrastructure.Data;
using Ghanavats.Repository;

namespace CleanArchitecture.Infrastructure.Repositories;

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
