using System;
using System.IO;
using System.Reflection;

namespace WindowsService.Play1
{
    public class ServiceLoggerFileBased : IServiceLogger
    {
        private readonly string _logFilePath;

        private const string LogFileName = "Log.txt";

        private const string Format = "{0} {1} {2}";

        public ServiceLoggerFileBased(string logFilePath = null)
        {
            if (string.IsNullOrWhiteSpace(logFilePath))
            {
                var logPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                _logFilePath = Path.Combine(logPath, LogFileName);
            }
            else
            {
                _logFilePath = logFilePath;
            }

        }

        public void Debug(string message)
        {
            WriteLine(Format, LogType.Debug, DateTime.Now.ToString("G"), message);
        }

        public void Error(string message, Exception exception = null)
        {
            WriteLine(Format, LogType.Error, DateTime.Now.ToString("G"), message);

            if (exception != null)
            {
                WriteLine(Format, LogType.Error, DateTime.Now.ToString("G"), $"{exception.Message} {Environment.NewLine} {exception.StackTrace}");
            }
        }

        public void Info(string message)
        {
            WriteLine(Format, LogType.Info, DateTime.Now.ToString("G"), message);
        }

        private void WriteLine(string format, params object[] args)
        {
            try
            {
                using (var writer = File.AppendText(_logFilePath))
                {
                    writer.WriteLine(format, args);
                    Console.WriteLine(format, args);
                }
            }
            catch {/*ignore*/}
        }

        internal static class LogType
        {
            public static string Info = "INFO";
            public static string Debug = "DEBUG";
            public static string Error = "ERROR";
        }
    }
}