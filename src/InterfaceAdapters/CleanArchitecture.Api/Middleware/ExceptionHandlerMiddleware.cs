using Microsoft.AspNetCore.Diagnostics;
using CleanArchitecture.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Middleware;

/// <summary>
/// Exception handling middleware
/// </summary>
public class ExceptionHandlerMiddleware : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Logger dependency</param>
    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger.CheckForNull();
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, CancellationToken cancellationToken)
    {
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server responded with error",
            Instance = httpContext.Request.Path,
            Detail = $"There has been a problem with your request. {exception.Message}",
            Type = exception.GetType().Name
        }, cancellationToken: cancellationToken);
        
        _logger.LogError("There has been a problem with your request.");

        return true;
    }
}
