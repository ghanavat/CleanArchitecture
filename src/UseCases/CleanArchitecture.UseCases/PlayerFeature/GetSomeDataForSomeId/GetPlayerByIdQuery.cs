using CleanArchitecture.Shared.Query;
using CleanArchitecture.Shared.ResultMechanism;
using CleanArchitecture.UseCases.Dtos;

namespace CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;

/// <summary>
/// A sample query to fetch some fake data
/// </summary>
/// <param name="SomeId"></param>
public record GetPlayerByIdQuery(string SomeId) : IQuery<Result<FilteredPlayerDto>>;
