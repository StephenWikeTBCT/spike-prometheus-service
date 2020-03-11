using System;

namespace prometheus_service_extensions
{
    public interface IBctMetricService
    {
        void GetCount();
        void GetDuration(Action action);
    }
}