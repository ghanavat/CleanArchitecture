using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Shared;

namespace CleanArchitecture.Infrastructure.Repositories;

/// <summary>
/// Do not use. This is a marker class to allow IRepository to be registered in composition root.
/// </summary>
/// <typeparam name="T">An entity to which the repository operations will be written for</typeparam>
#pragma warning disable CS1591
public class MarkerRepository<T> : RepositoryBase<T> where T : EntityBase, IAggregateRoot
{
    public MarkerRepository(SampleDbContext efContext) 
        : base(efContext)
    { }
}
