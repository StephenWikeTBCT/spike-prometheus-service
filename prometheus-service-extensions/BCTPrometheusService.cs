using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Net;

namespace prometheus_service_extensions
{
    public class BCTPrometheusService : IBctMetricService
    {
        IMetricServer server;
        private static readonly Counter ProcessedJobCount = Metrics.CreateCounter("app_jobs_processed_total", "Number of processed jobs.");
        private static readonly Histogram LoginDuration = Metrics.CreateHistogram("app_login_duration_seconds", "Histogram of login call processing durations.");

        public BCTPrometheusService(BctMetricsConfiguration configuration)
        {
            string hostname = configuration.Host;
            server = new MetricServer(hostname: hostname, port: 1234);
            server.Start();
        }

        #region Methods
        public void GetCount()
        {
            ProcessedJobCount.Inc();
        }

        public void GetDuration(Action action)
        {
            using (LoginDuration.NewTimer())
            {
                action();
            }
        }
        #endregion
    }

    public class BctMetricsConfiguration
    {
        public string Host { get; set; }
    }
}