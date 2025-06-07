using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using CleanArchitecture.Shared.Extensions;
using Ghanavats.ResultPattern;
using Ghanavats.ResultPattern.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Middlewares;

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
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        /* Handling Validation Exception */
        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            /* This can be an extension method */
            var errors = validationException.Errors.Select(validationError => new ValidationError
            {
                ErrorCode = validationError.ErrorCode,
                ErrorMessage = validationError.ErrorMessage,
                ValidationErrorType = (ValidationErrorType)validationError.Severity
            });

            await httpContext.Response.WriteAsJsonAsync(Result.Invalid(errors), new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower, false) }
            }, cancellationToken);
        }

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
