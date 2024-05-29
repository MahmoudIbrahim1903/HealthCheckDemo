using HealthCheckDemo.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace HealthCheckDemo
{
    public static class Utilities
    {
        public static IHealthChecksBuilder RegisterHealthCheck(this IServiceCollection services, string? conStr = null, string? redisConStr = null, string? rabbitMqConStr = null)
        {
           var healthCheckBuilder = services.AddHealthChecks();

            healthCheckBuilder.AddCheck<StartupHealthCheck>("Startup", tags: new[] { "ready delay" });

            if (!string.IsNullOrWhiteSpace(conStr))
                healthCheckBuilder.AddSqlServer(conStr, "Select 1", "Youxel db", HealthStatus.Unhealthy);

            healthCheckBuilder.AddDbContextCheck<AppDbContext>(); //call CanConnectAsync

            if (!string.IsNullOrWhiteSpace(redisConStr))
                healthCheckBuilder.AddRedis(redisConStr, "redis", HealthStatus.Unhealthy); //call CanConnectAsync

            if (!string.IsNullOrWhiteSpace(rabbitMqConStr))
                healthCheckBuilder.AddRabbitMQ(rabbitMqConStr, name: "RabbitMQ", failureStatus: HealthStatus.Unhealthy);

            healthCheckBuilder.AddCheck<SampleHealthCheck>(
                        "Sample",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: new[] { "sample" });


            healthCheckBuilder.AddCheck<SampleHealthCheck2>(
                        "Sample2",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: new[] { "sample2" });


            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Period = TimeSpan.FromSeconds(20);  // 30 seconds by default
                options.Predicate = healthCheck => healthCheck.Tags.Contains("sample");
            });

            services.AddSingleton<IHealthCheckPublisher, SampleHealthCheckPublisher>();

            return healthCheckBuilder;
        }

        public static void CustomHealthCheckMap(this WebApplication app)
        {
            const string healthCheckApiPath = "/healthzzz";

            // Configure Health Checks endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(healthCheckApiPath, new HealthCheckOptions
                {
                    ResponseWriter = WriteResponse
                });
            });
        }

        private static async Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                status = healthReport.Status.ToString(),
                entries = healthReport.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString(), duration = e.Value.Duration, exception = e.Value.Exception?.Message, description = e.Value.Description }),
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
