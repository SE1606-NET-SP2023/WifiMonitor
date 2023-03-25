using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiMonitor.Models;
using WifiMonitor.Utils;

namespace WifiMonitor.ViewModels
{
    public class NetworkWatcherVM
    {
        public static List<NetworkClientSnapshot> GetNetworkClients()
        {
            List<NetworkClientSnapshot> NetworkClients = new List<NetworkClientSnapshot>();
            var entry = Dns.GetHostEntry(Dns.GetHostName());
            List<IPAddress> Ips = entry.AddressList.ToList();
            NetworkClients = null;
            foreach (IPAddress ip in Ips)
            {
                NetworkClients.Add(new NetworkClientSnapshot
                {
                    IPAddress = ip.Address.ToString()
                });
            }
            return NetworkClients;
        }
    }
}
