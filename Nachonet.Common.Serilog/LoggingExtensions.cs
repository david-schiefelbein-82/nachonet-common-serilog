using Serilog;
using Serilog.Configuration;

namespace Nachonet.Common.Serilog
{
    public static class LoggingExtensions
    {
        public static LoggerConfiguration ThreadID(this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null)
                throw new ArgumentNullException(nameof(enrich));

            return enrich.With<ThreadIDEnricher>();
        }
    }
}
