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
        _logger.LogInformation($"SampleHealthCheckPublisher started {DateTime.Now}");

        if (report.Status == HealthStatus.Healthy)
        {
            _logger.LogInformation($"SampleHealthCheckPublisher the app is healthy {DateTime.Now}");
        }
        else
        {
            _logger.LogInformation($"SampleHealthCheckPublisher the app is healthy {DateTime.Now}");
        }

        _logger.LogInformation($"SampleHealthCheckPublisher finished {DateTime.Now}");

        return Task.CompletedTask;
    }
}