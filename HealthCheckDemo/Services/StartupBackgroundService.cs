namespace HealthCheckDemo.Services
{
    public class StartupBackgroundService : BackgroundService
    {
        private readonly StartupHealthCheck _healthCheck;

        public StartupBackgroundService(StartupHealthCheck startupHealthCheck)
        {
            _healthCheck = startupHealthCheck;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Simulate the effect of a long-running task.
            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);

            _healthCheck.StartupCompleted = true;
        }
    }
}
