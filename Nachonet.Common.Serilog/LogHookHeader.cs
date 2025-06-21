using Serilog.Sinks.File;
using System.Text;

namespace Nachonet.Common.Serilog
{
    public class LogHookHeader : FileLifecycleHooks
    {
        private const int DEFAULT_BUFFER_SIZE = 1024;
        private DateTime startTime;

        public LogHookHeader()
        {
            startTime = DateTime.Now;
        }

        public override Stream OnFileOpened(Stream underlyingStream, Encoding encoding)
        {
            var stream = base.OnFileOpened(underlyingStream, encoding);
            using (var writer = new StreamWriter(stream, encoding, DEFAULT_BUFFER_SIZE, true))
            {
                writer.WriteLine(string.Format("{0} ---------- Logging Started ----------", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff zzz")));
                var ass = System.Reflection.Assembly.GetEntryAssembly();
                var proc = System.Diagnostics.Process.GetCurrentProcess();
                writer.WriteLine("   PID         : " + proc.Id);
                writer.WriteLine("   SessionId   : " + proc.SessionId);
                if (ass != null)
                {
                    writer.WriteLine("   Assembly    : " + ass.FullName);
                }
                var args = Environment.GetCommandLineArgs();
                for (int i = 0; i < args.Length; i++)
                {
                    writer.WriteLine(string.Format("   Args[{0}]     : {1}", i, args[i]));
                }
                writer.WriteLine("   Domain      : " + Environment.UserDomainName);
                writer.WriteLine("   UserName    : " + Environment.UserName);
                writer.WriteLine("   MachineName : " + Environment.MachineName);
                writer.WriteLine("   OSVersion   : " + Environment.OSVersion);
                writer.WriteLine("   Start Time  : " + startTime.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"));
                writer.WriteLine("   Up Time     : " + (DateTime.Now - startTime).ToString("d\\.hh\\:mm\\:ss"));
                writer.WriteLine("   Current Dir : " + Environment.CurrentDirectory);
                writer.Flush();
                underlyingStream.Flush();
            }
            return stream;
        }

        public override Stream OnFileOpened(string path, Stream underlyingStream, Encoding encoding)
        {
            var stream = base.OnFileOpened(path, underlyingStream, encoding);
            
            return stream;
        }
    }
}
