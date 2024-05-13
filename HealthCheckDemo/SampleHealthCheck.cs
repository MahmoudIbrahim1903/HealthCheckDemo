using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class SampleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        Random rnd = new Random();
        var x = rnd.Next(1, 5);

        //throw new Exception("Test ex");
        if (x < 6)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result."));
    }
}

public class SampleHealthCheck2 : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        Random rnd = new Random();
        var x = rnd.Next(1, 5);

        //throw new Exception("Test ex");
        if (x < 6)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result."));
    }
}