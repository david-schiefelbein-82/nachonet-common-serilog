using Serilog.Core;
using Serilog.Events;

namespace Nachonet.Common.Serilog
{
    public class ThreadIDEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("ThreadID", GetThreadId())
            );
        }

        private string GetThreadId()
        {
            if (Thread.CurrentThread.Name == null)
                return Environment.CurrentManagedThreadId.ToString();
            else
                return string.Format("{0}/{1}", Environment.CurrentManagedThreadId.ToString(), Thread.CurrentThread.Name);
        }
    }
}
