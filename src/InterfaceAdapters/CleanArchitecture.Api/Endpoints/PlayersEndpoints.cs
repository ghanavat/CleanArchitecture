using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Requests;
using CleanArchitecture.UseCases.PlayerFeature.CreateNewPlayer;
using CleanArchitecture.UseCases.PlayerFeature.GetPlayerById;
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
            var query = new GetPlayerByIdQuery(playerId);
            var result = await mediator.Send(query);

            var mapResult = await result.ToResultAsync();
            return mapResult;
        }).WithName("GetPlayerById").WithDescription("Gets a player from the specified id");

        app.Players().MapPost("/new", async ([FromBody] CreatePlayerRequestModel requestModel, IMediator mediator) =>
        {
            var command = new CreatePlayerCommand(requestModel.FirstName, 
                requestModel.LastName, requestModel.Comment);

            var result = await mediator.Send(command);
            var mapResult = await result.ToResultAsync();
            return mapResult;
        }).WithName("CreatePlayer").WithDescription("Creates a new player");
    }
}
