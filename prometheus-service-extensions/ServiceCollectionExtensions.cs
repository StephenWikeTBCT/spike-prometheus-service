using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace prometheus_service_extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBCTMetrics(this IServiceCollection services, BctMetricsConfiguration configuration)
        {
            services.AddSingleton<IBctMetricService>(_ => new BCTPrometheusService(configuration));
            return services;
        }
    }
}
