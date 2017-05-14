using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService.Play1
{
    public partial class WindowsPlayService : ServiceBase
    {
        private static readonly IServiceLogger Logger = new ServiceLoggerFileBased();

        public WindowsPlayService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Debug("Starting up the service...");
            /*
             your service startup logic goes here. do not block, return imediately, start a task perhaps to 
             perform all the startup overhead.

            There are ways to request more time during on start or onstop, see the article in this project HowTo-Article.pdf
             */
            var task = new Task(() =>
              {
                  try
                  {
                      Thread.Sleep(10000);
                      throw new Exception("something went wrong...");
                  }
                  catch (Exception e)
                  {
                      Logger.Error("This should trigger service Stop.", e);
                      this.StopService();
                  }
              });

            task.Start();
        }

        protected override void OnStop()
        {
            Logger.Debug("Stopping the service...");
        }

        public void StartService()
        {
            //only significant if you are running it as a console app, otherwise no real use here
            OnStart(null);
        }

        public void StopService()
        {
            Stop();
        }
    }
}
