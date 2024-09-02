namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// An example interface in which domain related operations defined.
/// </summary>
public interface ISampleDoingSomethingService<T> where T : class
{
    Task<T> GetSomeData();
}
