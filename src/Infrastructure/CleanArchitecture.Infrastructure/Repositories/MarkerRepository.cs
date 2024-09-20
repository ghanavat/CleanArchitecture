using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.Repositories;

/// <summary>
/// Do not use. This is a marker class to allow IRepository be registered in composition root.
/// </summary>
/// <typeparam name="T">An entity to which the repository operations will be written for</typeparam>
public class MarkerRepository<T> : RepositoryBase<T> where T : class, IAggregateRoot
{
    public MarkerRepository(SampleDbContext efContext) 
        : base(efContext)
    { }
}
