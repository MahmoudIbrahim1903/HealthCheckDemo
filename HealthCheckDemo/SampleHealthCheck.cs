using Microsoft.Extensions.Diagnostics.HealthChecks;

public class SampleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        Random rnd = new Random();
        var x = rnd.Next(1, 10);

        if (x < 5)
            return Task.FromResult(HealthCheckResult.Unhealthy($"An unhealthy result, as the random number is {x}"));

        return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
    }
}

public class SampleHealthCheck2 : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        Random rnd = new Random();
        var x = rnd.Next(1, 10);

        if (x < 5)
            return Task.FromResult(HealthCheckResult.Unhealthy($"An unhealthy result, as the random number is {x}"));

        return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
    }
}