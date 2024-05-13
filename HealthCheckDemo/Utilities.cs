using HealthCheckDemo.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckDemo
{
    public static class Utilities
    {
        public static IServiceCollection RegisterHealthCheck(this IServiceCollection services, string conStr)
        {
            services.AddHealthChecks();

            services.AddHealthChecks()
                .AddCheck<StartupHealthCheck>("Startup", tags: new[] { "ready delay" });

            services.AddHealthChecks()
                .AddSqlServer(conStr, "Select 1", "Youxel db", HealthStatus.Unhealthy);

            services.AddHealthChecks()
                .AddDbContextCheck<AppDbContext>(); //call CanConnectAsync

            services.AddHealthChecks()
                    .AddCheck<SampleHealthCheck>(
                        "Sample",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: new[] { "sample" });


            services.AddHealthChecks()
                    .AddCheck<SampleHealthCheck2>(
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

            return services;
        }

        public static IServiceCollection RegisterHealthCheckUI(this IServiceCollection services, string conStr)
        {
            services.AddHealthChecksUI()
                .AddSqlServerStorage(conStr);
                
            return services;
        }

        public static void CustomHealthCheckMap(this WebApplication app)
        {
            const string healthCheckApiPath = "/healthzzz";

            // Configure Health Checks UI
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = $"{healthCheckApiPath}-ui";
            });

            // Configure Health Checks endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(healthCheckApiPath);
                endpoints.MapHealthChecksUI();
            });
        }
    }
}
