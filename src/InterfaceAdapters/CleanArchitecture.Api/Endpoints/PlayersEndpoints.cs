using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Requests;
using CleanArchitecture.UseCases.PlayerFeature.Create;
using CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;
using Ghanavats.ResultPattern.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Endpoints;

public static class PlayersEndpoints
{
    public static void Map(WebApplication app)
    {
        app.Players().MapGet("/{playerId:int}", async ([FromRoute] int playerId, IMediator mediator) =>
        {
            var query = new GetPlayerByIdQuery(playerId, string.Empty);
            var result = await mediator.Send(query);

            var mapResult = await result.ToResultAsync();
            return mapResult;
        });
        
        app.Players().MapPost("/new", async ([FromBody] CreatePlayerRequestModel requestModel, IMediator mediator) =>
        {
            var command = new CreatePlayerCommand(requestModel.FirstName, requestModel.Lastname,
                requestModel.Comment);

            var result = await mediator.Send(command);
            return result.ToResultAsync();
        });
    }
}
