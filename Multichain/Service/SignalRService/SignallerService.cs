using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;

using Microsoft.Owin.Hosting;

using Stoneycreek.Libraries.Blockchain.SignallR;

namespace Stoneycreek.Services.Blockchain.SignalRService
{
    public partial class SignallerService : ServiceBase
    {
        public System.Timers.Timer time;

        public SignallerService()
        {
            this.InitializeComponent();
        }
  protected override void OnStart(string[] args)
        {
            // <add key="IPAddress" value="10.1.5.111"/>
            // <add key="Port" value="10000"/>
            // <add key="Connector" value="raw-connection"/>

            this.time = new System.Timers.Timer();
            this.time.Interval = 500;
            this.time.Elapsed += (sender, eventArgs) => { this.PingTing(); };

            this.time.Start();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var ip = config.AppSettings.Settings["IPAddress"].Value;
            var port = config.AppSettings.Settings["Port"].Value;
            var connector = config.AppSettings.Settings["Connector"].Value;
            string url = string.Format("http://{0}:{1}/{2}", ip, port, connector);

            File.WriteAllText(@"D:\Development\Eigenbouw\bc\Ethereum.NET-master\Service\SignalRService\bin\Debug\debug.txt", url);
            try
            {
                using (WebApp.Start<Startup>(url))
                {
                    do
                    {
                        this.WriteInfo("bla");
                    }
                    while (true);
      
                }
            }
            catch (Exception ex)
            {
                this.WriteInfo(ex.Message);
            }
            
        }

        private void PingTing()
        {
            this.WriteInfo(DateTime.Now.ToString());
        }

        private void WriteInfo(string info)
        {
            File.AppendAllLines(
                @"D:\Development\Eigenbouw\bc\Ethereum.NET-master\Service\SignalRService\bin\Debug\debug.txt",
                new string[] { info });
        }
        protected override void OnStop()
        {
        }
    }
}
