using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using prometheus_service_extensions;

namespace prometheus_example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddBCTMetrics(new BctMetricsConfiguration()
                    {
                        Host = "localhost"
                    });
                });
    }
}
