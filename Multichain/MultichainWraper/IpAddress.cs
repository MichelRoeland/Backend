using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class IpAddress
    {
        public static string GetLocalIPAddress()
        {
            string ipAddress;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = host.AddressList.First(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();

            if (ipAddress == null)
            {
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }

            return ipAddress;
        }
    }
}
