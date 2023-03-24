using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiMonitor.Models
{
    class WifiDetails
    {
        public string? SSID { get; set; }
        public string? Channel { get; set; }
        public string? Frequency { get; set; }
        public string? Bandwidth { get; set; }
        public string? Protocol { get; set; }
        public string? BSSID { get; set; }
        public string? PrivateIPv4 { get; set; }
        public string? PrivateSubnet { get; set; }
        public string? PublicIPv4 { get; set; }
        public string? Authentication { get; set; }
        public string? Encryption { get; set; }
        public string? Kind { get; set; }
        public string? Connectivity { get; set; }
        public string? Interface { get; set; }
    }
}
