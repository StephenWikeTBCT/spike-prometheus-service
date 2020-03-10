using Microsoft.Extensions.DependencyInjection;
using System;

namespace prometheus_service_extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBCTMetrics(this IServiceCollection services)
        {
            services.AddSingleton(_ => new BCTPrometheusService());
            return services;
        }
    }
}
