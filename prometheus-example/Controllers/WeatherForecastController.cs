using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus;

namespace prometheus_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private IMetricServer metricServer = new MetricServer(hostname: "localhost", port: 1234);
        private static readonly Counter TickTock = Metrics.CreateCounter("weather_forecast_controller_counter", "Counting ticks...");
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            metricServer.Start();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var pusher = new MetricPusher(new MetricPusherOptions
            {
                Endpoint = "https://pushgateway.example.org:9091/metrics",
                Job = "some_job"
            });

            while (true)
            {
                //pusher.Start();
                TickTock.Inc();
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
