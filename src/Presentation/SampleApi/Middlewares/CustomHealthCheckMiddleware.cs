using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SampleApi.Middlewares;

/// <summary>
/// Custom health check middleware
/// </summary>
public class CustomHealthCheckMiddleware : IHealthCheck
{
    /// <summary>
    /// Implementation of custom health check
    /// </summary>
    /// <param name="context">Health check context.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Health Check Result</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
