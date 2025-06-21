using Serilog.Core;
using Serilog.Events;

namespace Nachonet.Common.Serilog
{
    public class ThreadIDEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
              "ThreadID", Thread.CurrentThread.Name ?? Thread.CurrentThread.ManagedThreadId.ToString()));
        }
    }
}
