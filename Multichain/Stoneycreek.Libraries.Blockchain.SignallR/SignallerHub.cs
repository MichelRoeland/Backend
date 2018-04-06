using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
namespace Stoneycreek.Libraries.Blockchain.SignallR
{
    public class SignallerHub : Hub
    {
        private Timer t = null;
        private StateObjClass StateObj = new StateObjClass();
        private TimerCallback TimerDelegate = new System.Threading.TimerCallback(TimerTask);

        private static dynamic clientscontext = null;
        private static List<string> clients = null;


        private static int run = 1;
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        public bool Heartbeat()
        {
            return true;
        }

        public void AddMessage(string message, string test)
        {
            Clients.All.send(message);
        }

        public void DetermineLength(string message)
        {
            Console.WriteLine(message);
            // Clients.All.Send("Client callback");
            var myConnectionId = Context.ConnectionId;

            if (clients == null)
            {
                clients = new List<string>();
                t = new System.Threading.Timer(TimerDelegate, StateObj, 1000, 1000);
                clientscontext = Clients;
            }

            clients.Add(myConnectionId);
        }

        private static void TimerTask(object StateObj)
        {
            // StateObjClass State = (StateObjClass)StateObj;
            switch (run)
            {
                case 1:
                    clientscontext.Client(clients[0]).Send("Send message to client 1\r\n");
                    break;
                case 2:
                    if (clients.Count > 1)
                    {
                        clientscontext.Client(clients[1]).Send("Send message to client 2\r\n");
                    }

                    break;
                case 3:
                    clientscontext.All.Send("Send callback message to ALL!\r\n");
                    break;
                default:
                    // clientscontext.All.Close("Send callback message to ALL!\r\n");
                    run = 0;
                    break;
            }

            run += 1;
        }
    }
}
