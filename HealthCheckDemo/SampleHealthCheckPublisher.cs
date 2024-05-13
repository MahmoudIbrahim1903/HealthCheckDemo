using Microsoft.Extensions.Diagnostics.HealthChecks;

public class SampleHealthCheckPublisher : IHealthCheckPublisher
{
    private readonly ILogger<SampleHealthCheckPublisher> _logger;

    public SampleHealthCheckPublisher(ILogger<SampleHealthCheckPublisher> logger)
    {
        _logger = logger;
    }
    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"SampleHealthCheckPublisher started {DateTime.Now.ToLongTimeString()}");

        if (report.Status == HealthStatus.Healthy)
        {
            _logger.LogInformation($"SampleHealthCheckPublisher the app is healthy {DateTime.Now.ToLongTimeString()}");
        }
        else
        {
            _logger.LogInformation($"SampleHealthCheckPublisher the app is healthy {DateTime.Now.ToLongTimeString()}");
        }

        _logger.LogInformation($"SampleHealthCheckPublisher finished {DateTime.Now.ToLongTimeString()}");

        return Task.CompletedTask;
    }
}