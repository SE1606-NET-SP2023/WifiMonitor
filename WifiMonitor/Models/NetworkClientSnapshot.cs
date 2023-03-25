using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiMonitor.Models
{
    public class NetworkClientSnapshot
    {
        public string? IPAddress { get; set; }
        public string? DeviceName { get; set; }
        public string? MACAddress { get; set; }
        public string? Vendor { get; set; }
        public string? DeviceInfo { get; set; }
        public string? UserName { get; set; }
        public DateTime? DetectedTime { get; set; }
        public bool? IsActive { get; set; }
    }
}
