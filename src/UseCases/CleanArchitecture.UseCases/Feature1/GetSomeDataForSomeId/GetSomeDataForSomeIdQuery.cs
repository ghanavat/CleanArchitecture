using CleanArchitecture.Shared.Query;
using CleanArchitecture.Shared.ResultMechanism;

namespace CleanArchitecture.UseCases.Feature1.GetSomeDataForSomeId;

/// <summary>
/// A sample query to fetch some fake data
/// </summary>
/// <param name="SomeId"></param>
public record GetSomeDataForSomeIdQuery(string SomeId) : IQuery<Result<SampleFilteredWithIdDto>>;
