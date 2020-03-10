using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using static Microsoft.Extensions.Hosting.Host;
using prometheus_service_extensions;

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
                services.AddBCTMetrics();
            });
    }
}
