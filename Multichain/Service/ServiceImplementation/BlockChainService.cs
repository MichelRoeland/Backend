using System;
using System.Configuration;
using System.ServiceModel.Channels;
using Microsoft.AspNet.SignalR.Client;
using Stoneycreek.libraries.MultichainWrapper.Structs;
using Stoneycreek.Services.Blockchain.ServiceContracts;

namespace Stoneycreek.Services.Blockchain.ServiceImplementation
{
    //    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    //    [ServiceBehavior(Name = "WcfPushRequestService", Namespace = "http://services.suntech.com/WcfPushRequestService/1.0")]
    public class BlockChainService : IBlockChainService
    {
        #region Public properties

        public string SddSchema { get; set; }

        #endregion

        public KeyPair RequestAddress(string xsdrequest)
        {
            // Er wordt een adres gevraagd door een client.
            // Deze client moet zich kunnen identificeren
            // maw een  identificatiecode moet worden meegegeven vanaf de client.
            // we geven ook een SignalR code mee!

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var ip = config.AppSettings.Settings["IPAddress"].Value;
            var port = config.AppSettings.Settings["Port"].Value;
            var connector = config.AppSettings.Settings["Connector"].Value;
            string url = string.Format("http://{0}:{1}/{2}", ip, port, connector);

            var hubConnection = new HubConnection(url);
            hubConnection.TraceLevel = TraceLevels.All;
            hubConnection.TraceWriter = Console.Out;
            IHubProxy myHubProxy = hubConnection.CreateHubProxy("MyHub");

            myHubProxy.On<string, string>("addMessage", (name, message) => Console.Write("Recieved addMessage: " + name + ": " + message + "\n"));
            myHubProxy.On("heartbeat", () => Console.Write("Recieved heartbeat \n"));
            // myHubProxy.On<HelloModel>("sendHelloObject", hello => Console.Write("Recieved sendHelloObject {0}, {1} \n", hello.Molly, hello.Age));

            hubConnection.Start().Wait();

            //myHubProxy.Invoke("DetermineLength", "asfsad;lfkaslkflkda").Wait();
            myHubProxy.On<string>("AddMessage", delegate (string message) { Console.Write(message); });

            while (true)
            {
                string key = Console.ReadLine();
                if (key.ToUpper() == "W")
                {
                    myHubProxy.Invoke("DetermineLength", "Client 2 connecting").Wait();
                }
            }



            throw new NotImplementedException();
        }

        public Message ExecuteRequest(Message request)
        {
            throw new NotImplementedException();
        }

        public object ExecuteRequestMapData(string data)
        {
            throw new NotImplementedException();
        }

        public bool ExeQueuete()
        {
            throw new NotImplementedException();
        }
    }
}