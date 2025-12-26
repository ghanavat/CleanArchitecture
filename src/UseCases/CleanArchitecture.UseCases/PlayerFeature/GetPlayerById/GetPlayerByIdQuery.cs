using CleanArchitecture.Shared.Query;
using CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId.Dtos;
using Ghanavats.ResultPattern;

namespace CleanArchitecture.UseCases.PlayerFeature.GetPlayerById;

/// <summary>
/// Query for fetching player details
/// </summary>
/// <param name="PlayerId"></param>
public record GetPlayerByIdQuery(int PlayerId) : IQuery<Result<FilteredPlayerDto>>;
