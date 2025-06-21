using Serilog.Sinks.File;

namespace Nachonet.Common.Serilog
{
    /// <summary>
    /// This class contains static methods that allow you to log a header, but also archive old logs into ZIP files.
    /// 
    /// I wish there was a better way... but there are static methods that are essentially arguments for how many days to keep a ZIP file
    /// I've implemented 1-31,60,90,120,365 days.  If you want anything else you'd need to implement your own static methods in a class
    /// </summary>
    public class Hooks
    {
        private static LogHookHeader headerLogHook = new LogHookHeader();

        public static FileLifecycleHooks LogHeader => headerLogHook;
        public static FileLifecycleHooks LogHeaderThenArchive1 => headerLogHook.Then(LogArchive1);
        public static FileLifecycleHooks LogHeaderThenArchive2 => headerLogHook.Then(LogArchive2);
        public static FileLifecycleHooks LogHeaderThenArchive3 => headerLogHook.Then(LogArchive3);
        public static FileLifecycleHooks LogHeaderThenArchive4 => headerLogHook.Then(LogArchive4);
        public static FileLifecycleHooks LogHeaderThenArchive5 => headerLogHook.Then(LogArchive5);
        public static FileLifecycleHooks LogHeaderThenArchive6 => headerLogHook.Then(LogArchive6);
        public static FileLifecycleHooks LogHeaderThenArchive7 => headerLogHook.Then(LogArchive7);
        public static FileLifecycleHooks LogHeaderThenArchive8 => headerLogHook.Then(LogArchive8);
        public static FileLifecycleHooks LogHeaderThenArchive9 => headerLogHook.Then(LogArchive9);
        public static FileLifecycleHooks LogHeaderThenArchive10 => headerLogHook.Then(LogArchive10);
        public static FileLifecycleHooks LogHeaderThenArchive11 => headerLogHook.Then(LogArchive11);
        public static FileLifecycleHooks LogHeaderThenArchive12 => headerLogHook.Then(LogArchive12);
        public static FileLifecycleHooks LogHeaderThenArchive13 => headerLogHook.Then(LogArchive13);
        public static FileLifecycleHooks LogHeaderThenArchive14 => headerLogHook.Then(LogArchive14);
        public static FileLifecycleHooks LogHeaderThenArchive15 => headerLogHook.Then(LogArchive15);
        public static FileLifecycleHooks LogHeaderThenArchive16 => headerLogHook.Then(LogArchive16);
        public static FileLifecycleHooks LogHeaderThenArchive17 => headerLogHook.Then(LogArchive17);
        public static FileLifecycleHooks LogHeaderThenArchive18 => headerLogHook.Then(LogArchive18);
        public static FileLifecycleHooks LogHeaderThenArchive19 => headerLogHook.Then(LogArchive19);
        public static FileLifecycleHooks LogHeaderThenArchive20 => headerLogHook.Then(LogArchive20);
        public static FileLifecycleHooks LogHeaderThenArchive21 => headerLogHook.Then(LogArchive21);
        public static FileLifecycleHooks LogHeaderThenArchive22 => headerLogHook.Then(LogArchive22);
        public static FileLifecycleHooks LogHeaderThenArchive23 => headerLogHook.Then(LogArchive23);
        public static FileLifecycleHooks LogHeaderThenArchive24 => headerLogHook.Then(LogArchive24);
        public static FileLifecycleHooks LogHeaderThenArchive25 => headerLogHook.Then(LogArchive25);
        public static FileLifecycleHooks LogHeaderThenArchive26 => headerLogHook.Then(LogArchive26);
        public static FileLifecycleHooks LogHeaderThenArchive27 => headerLogHook.Then(LogArchive27);
        public static FileLifecycleHooks LogHeaderThenArchive28 => headerLogHook.Then(LogArchive28);
        public static FileLifecycleHooks LogHeaderThenArchive29 => headerLogHook.Then(LogArchive29);
        public static FileLifecycleHooks LogHeaderThenArchive30 => headerLogHook.Then(LogArchive30);
        public static FileLifecycleHooks LogHeaderThenArchive31 => headerLogHook.Then(LogArchive31);
        public static FileLifecycleHooks LogHeaderThenArchive60 => headerLogHook.Then(LogArchive60);
        public static FileLifecycleHooks LogHeaderThenArchive90 => headerLogHook.Then(LogArchive90);
        public static FileLifecycleHooks LogHeaderThenArchive120 => headerLogHook.Then(LogArchive120);
        public static FileLifecycleHooks LogHeaderThenArchive365 => headerLogHook.Then(LogArchive365);

        private static IDictionary<int, LogHookArchiver> logHooks = new Dictionary<int, LogHookArchiver>();

        public static LogHookArchiver LogArchive1 => Get(1);
        public static LogHookArchiver LogArchive2 => Get(2);
        public static LogHookArchiver LogArchive3 => Get(3);
        public static LogHookArchiver LogArchive4 => Get(4);
        public static LogHookArchiver LogArchive5 => Get(5);
        public static LogHookArchiver LogArchive6 => Get(6);
        public static LogHookArchiver LogArchive7 => Get(7);
        public static LogHookArchiver LogArchive8 => Get(8);
        public static LogHookArchiver LogArchive9 => Get(9);
        public static LogHookArchiver LogArchive10 => Get(10);
        public static LogHookArchiver LogArchive11 => Get(11);
        public static LogHookArchiver LogArchive12 => Get(12);
        public static LogHookArchiver LogArchive13 => Get(13);
        public static LogHookArchiver LogArchive14 => Get(14);
        public static LogHookArchiver LogArchive15 => Get(15);
        public static LogHookArchiver LogArchive16 => Get(16);
        public static LogHookArchiver LogArchive17 => Get(17);
        public static LogHookArchiver LogArchive18 => Get(18);
        public static LogHookArchiver LogArchive19 => Get(19);
        public static LogHookArchiver LogArchive20 => Get(20);
        public static LogHookArchiver LogArchive21 => Get(21);
        public static LogHookArchiver LogArchive22 => Get(22);
        public static LogHookArchiver LogArchive23 => Get(23);
        public static LogHookArchiver LogArchive24 => Get(24);
        public static LogHookArchiver LogArchive25 => Get(25);
        public static LogHookArchiver LogArchive26 => Get(26);
        public static LogHookArchiver LogArchive27 => Get(27);
        public static LogHookArchiver LogArchive28 => Get(28);
        public static LogHookArchiver LogArchive29 => Get(29);
        public static LogHookArchiver LogArchive30 => Get(30);
        public static LogHookArchiver LogArchive31 => Get(31);
        public static LogHookArchiver LogArchive60 => Get(60);
        public static LogHookArchiver LogArchive90 => Get(90);
        public static LogHookArchiver LogArchive120 => Get(120);
        public static LogHookArchiver LogArchive365 => Get(365);

        private static LogHookArchiver Get(int zipFilesToKeep)
        {
            LogHookArchiver? logHook;
            if (!logHooks.TryGetValue(zipFilesToKeep, out logHook))
            {
                logHook = new LogHookArchiver(zipFilesToKeep);
                logHooks[zipFilesToKeep] = logHook;
            }

            return logHook;
        }
    }
}
