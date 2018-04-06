using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;

using Owin;
namespace Stoneycreek.Libraries.Blockchain.SignallR
{
    public class SignallerServer
    {
        public void StartServer()
        {
            // <add key="IPAddress" value="10.1.5.111"/>
            // <add key="Port" value="10000"/>
            // <add key="Connector" value="raw-connection"/>

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var ip = config.AppSettings.Settings["IPAddress"].Value;
            var port = config.AppSettings.Settings["Port"].Value;
            var connector = config.AppSettings.Settings["Connector"].Value;
            string url = string.Format("http://{0}:{1}/{2}", ip, port, connector);

            WriteInfo(url);
            try
            {
                WebApp.Start<Startup>(url);
            }
            catch (Exception ex)
            {
                WriteInfo(ex.Message);
            }
        }
        public void WriteInfo(string info)
        {
            File.AppendAllLines(
                @"D:\Development\Eigenbouw\bc\Ethereum.NET-master\Service\SignalRService\bin\Debug\debug.txt",
                new string[] { info });
        }
    }
    
    
}
