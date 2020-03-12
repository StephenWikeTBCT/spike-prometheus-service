using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using static Microsoft.Extensions.Hosting.Host;
using prometheus_service_extensions;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;

namespace prometheus_example_host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.AddEnvironmentVariables();
            })
            .ConfigureServices(services =>
            {
                services.AddBCTMetrics(new BctMetricsConfiguration() 
                {
                    Host = "prometheus-example-host"
                });
                services.AddHostedService<DoSomething>();
            });
    }

    public class DoSomething : IHostedService
    {
        private IBctMetricService _metrics;

        public DoSomething(IBctMetricService metrics)
        {
            _metrics = metrics;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _metrics.GetCount();
            _metrics.GetDuration(() => Thread.Sleep(4000));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}