using Asp.Versioning;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Shared.ResultMechanism;
using CleanArchitecture.UseCases.Dtos;
using CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Requests;

namespace SampleApi.Controllers;

/// <summary>
/// Sample Controller for the Clean Architecture proposal
/// </summary>
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[ApiVersion("3.0")]
public class PlayerController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Sample Controller constructor
    /// </summary>
    /// <param name="mediator">Mediator dependency for sending Commands and Queries</param>
    public PlayerController(IMediator mediator)
    {
        _mediator = mediator.CheckForNull();
    }

    /// <summary>
    /// Sample Get endpoint.
    /// It has 'Authorize' attribute and API Versioning.
    /// </summary>
    /// <returns>OK or BadRequest</returns>
    [HttpGet("{playerId:int}")]
    [Authorize(Policy = "SamplePolicy", Roles = "SampleRole")]
    [MapToApiVersion("2.0")]
    public Task<Result<FilteredPlayerDto>> GetPlayerByIdAsync([FromRoute] int playerId)
    {
        var query = new GetPlayerByIdQuery(playerId);
        return _mediator.Send(query);
    }

    /// <summary>
    /// A sample endpoint with POST.
    /// POST is equivalent to CREATE in CRUD.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost("example_something_post/new")]
    [MapToApiVersion("3.0")]
    public Task<IActionResult> PostSomethingAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// A sample endpoint wth PUT.
    /// PUT is used for both creating and updating, but primarily it does UPDATE in CRUD.
    /// When the reference to the resource exists, UPDATE operations happen, otherwise CREATE.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut("example_something_put/new")]
    [MapToApiVersion("1.0")]
    public Task<IActionResult> CreateSomethingAsync([FromBody] UpdateSampleRequestModel requestModel)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// A sample endpoint with PATCH.
    /// PATCH is used for partially update a resource.
    /// </summary>
    /// <param name="someId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPatch("example_something_patch/{someId:int}")]
    [MapToApiVersion("1.0")]
    public Task<IActionResult> UpdateSomething([FromRoute] int someId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// A sample endpoint with DELETE.
    /// It is used to delete a specified resource.
    /// </summary>
    /// <param name="someId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpDelete("example_something_delete/{someId:int}")]
    [MapToApiVersion("1.0")]
    public Task<IActionResult> DeleteSomething([FromRoute] int someId)
    {
        throw new NotImplementedException();
    }
}