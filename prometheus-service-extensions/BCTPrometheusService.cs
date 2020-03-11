using Prometheus;
using System;
using System.Net;

namespace prometheus_service_extensions
{
    public class BCTPrometheusService : IBctMetricService
    {
        IMetricServer server = new MetricServer(hostname: "localhost", port: 1234);
        private static readonly Counter ProcessedJobCount = Metrics.CreateCounter("app_jobs_processed_total", "Number of processed jobs.");
        private static readonly Histogram LoginDuration = Metrics.CreateHistogram("app_login_duration_seconds", "Histogram of login call processing durations.");

        public BCTPrometheusService()
        {
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
}