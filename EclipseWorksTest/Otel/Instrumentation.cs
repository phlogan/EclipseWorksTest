using System.Diagnostics.Metrics;

namespace EclipseWorksTest.Otel
{
    public class Instrumentation
    {
        private readonly Meter _meter;
        public const string MeterName = "EclipseMeter";
        public Instrumentation()
        {
            _meter = new Meter(MeterName, "1.0.0");
            CreatedProjectsCounter = _meter.CreateCounter<long>("created_projects");
            ProjectCreationProcessTime = _meter.CreateHistogram<double>("project_creation_process_time");
        }

        public Counter<long> CreatedProjectsCounter { get; }
        public Histogram<double> ProjectCreationProcessTime { get; }

    }
}
