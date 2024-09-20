using Asp.Versioning;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Shared.ResultMechanism;
using CleanArchitecture.UseCases.Dtos;
using CleanArchitecture.UseCases.PlayerFeature.GetSomeDataForSomeId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SampleApi.Requests;

namespace SampleApi.Controllers;

/// <summary>
/// Sample Controller for the Clean Architecture proposal
/// </summary>
[ApiController]
[Route("[controller]/v{version:apiVersion}")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[ApiVersion("3.0")]
public class SampleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SampleController> _logger;

    /// <summary>
    /// Sample Controller constructor
    /// </summary>
    /// <param name="mediator">Mediator dependency to send Commands and Queries</param>
    /// <param name="logger">Logger dependency</param>
    /// <exception cref="InvalidCastException"></exception>
    public SampleController(IMediator mediator, ILogger<SampleController> logger)
    {
        _mediator = mediator.CheckForNull();
        _logger = logger.CheckForNull();
    }

    /// <summary>
    /// Sample Get endpoint.
    /// It has 'Authorize' attribute and API Versioning.
    /// </summary>
    /// <returns>OK or BadRequest</returns>
    [HttpGet("example_something_get/{someId}")]
    [Authorize(Policy = "SamplePolicy", Roles = "SampleRole")]
    //[ProducesResponseType(typeof(Result<SampleFilteredWithIdDto>), StatusCodes.Status200OK)]
    //[SwaggerResponse(StatusCodes.Status200OK, typeof(Result<SampleFilteredWithIdDto>))]
    [MapToApiVersion("2.0")]
    public Task<Result<FilteredPlayerDto>> GetSomethingAsync([FromRoute] string someId) /* We don't have to use IActionResult return type, if only one type is returned. */
    {
        var query = new GetPlayerByIdQuery(someId);
        return _mediator.Send(query);
    }

    /// <summary>
    /// A sample endpoint with POST.
    /// POST is equivalent to CREATE in CRUD.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost("example_something_post/new")]
    [SwaggerResponse(StatusCodes.Status200OK, typeof(Result<>))]
    [MapToApiVersion("3.0")]
    public Task<IActionResult> PostSomethingAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// A sample endpoint wth PUT.
    /// PUT is used for both creating and updating, but primarily it does UPDATE in CRUD.
    /// When the reference to the resource exists, an UPDATE operations happens, otherwise CREATE.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut("example_something_put/new")]
    [SwaggerResponse(StatusCodes.Status200OK, typeof(Result<>))]
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
    [SwaggerResponse(StatusCodes.Status200OK, typeof(Result<>))]
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
    [SwaggerResponse(StatusCodes.Status200OK, typeof(Result<>))]
    [MapToApiVersion("1.0")]
    public Task<IActionResult> DeleteSomething([FromRoute] int someId)
    {
        throw new NotImplementedException();
    }
}
