using Serilog.Debugging;
using Serilog.Sinks.File;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Nachonet.Common.Serilog
{
    public class LogHookArchiver : FileLifecycleHooks
    {
        private static readonly Regex ArchiveRegex = new Regex("^(.*)([0-9]{8})\\.log.zip$");

        private const int DEFAULT_BUFFER_SIZE = 1024;
        private readonly int _zipFilesToKeep;

        public LogHookArchiver()
        {
            _zipFilesToKeep = 30;
        }

        public LogHookArchiver(int zipFilesToKeep)
        {
            _zipFilesToKeep = zipFilesToKeep >= 0 ? zipFilesToKeep : 1;
        }

        public override void OnFileDeleting(string path)
        {
            FileInfo? zipFile = null;
            try
            {
                FileInfo file = new FileInfo(Path.GetFullPath(path));
                zipFile = new FileInfo(file.FullName + ".zip");

                if (zipFile.Exists)
                    return;


                using (ZipArchive archive = ZipFile.Open(zipFile.FullName, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(file.FullName, file.Name, CompressionLevel.Fastest);
                }

            }
            catch (Exception ex)
            {
                // if something went wrong then take no action
                SelfLog.WriteLine("OnFileDeleting {0} {1}", zipFile, ex);
            }
            finally
            {
                if (zipFile != null)
                    DeleteOldArchives(zipFile, _zipFilesToKeep);
            }

        }

        private static void DeleteOldArchives(FileInfo zipFile, int zipFilesToKeep)
        {
            try
            {
                var dir = zipFile.Directory;
                if (dir == null)
                {
                    return;
                }

                var match = ArchiveRegex.Match(zipFile.Name);
                if (match != null && match.Success)
                {
                    string prefix = match.Groups[1].Value;
                    string timestamp = match.Groups[2].Value;
                    DateTime day = DateTime.Today;

                    // go back a month incase the program hasn't run in a while
                    int oldestToCheck = 31;
                    day = day.AddDays(-1 * zipFilesToKeep);
                    for (int i = 0; i < oldestToCheck; ++i)
                    {
                        FileInfo checkFile = new FileInfo(Path.Combine(dir.FullName, String.Format("{0}{1}.log.zip", prefix, day.ToString("yyyyMMdd"))));
                        if (checkFile.Exists)
                        {
                            SelfLog.WriteLine("Deleting archive file {0}", checkFile.FullName);
                            checkFile.Delete();
                        }

                        day = day.AddDays(-1);
                    }
                }
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("DeleteOldArchives {0} {1}", zipFile, ex);
                // if something went wrong then take no action
            }
        }
    }

}
