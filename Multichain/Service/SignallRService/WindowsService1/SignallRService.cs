using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Stoneycreek.Libraries.Blockchain.SignallR;

namespace Stoneycreek.Services.Blockchain.SignallRService
{
    public partial class SignallRService : ServiceBase
    {
        private Timer t = null;

        private int counter;
        public SignallRService()
        {
            InitializeComponent();
            this.ServiceName = "SignallRService";
        }

        protected override void OnStart(string[] args)
        {
            SignallerServer server = new SignallerServer();

            this.t = new Timer();
            this.t.Interval = 5000;
            this.t.Elapsed += (sender, eventArgs) =>
                {
                    server.WriteInfo("Tick " + this.counter);
                    this.counter += 1;
                    if (this.counter > 50)
                    {
                        server.WriteInfo("Stop service");
                        this.Stop();
                    }
                };

            server.WriteInfo("Start service");
            server.StartServer();
        }

        protected override void OnStop()
        {
        }
    }
}
