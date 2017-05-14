using System;
using System.ServiceProcess;
using System.Threading;

namespace WindowsService.Play1
{
    internal static class Program
    {
        //a simple file logger for the service
        private static readonly IServiceLogger Logger = new ServiceLoggerFileBased();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                StartAsConsoleApp();
            }
            else
            {
                StartAsService();
            }
        }

        private static void StartAsService()
        {
            var servicesToRun = new ServiceBase[]
            {
                new WindowsPlayService(),
            };
            ServiceBase.Run(servicesToRun);
        }

        private static void StartAsConsoleApp()
        {
            var service = new WindowsPlayService();

            service.StartService();

            Console.ReadKey(false);

            Logger.Debug("Stopping...");

            Thread.Sleep(1000);
        }

    }
}
