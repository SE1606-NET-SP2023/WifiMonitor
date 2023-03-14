using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiMonitor.Models
{
    public class WifiInformation
    {
        public string? SSID { get; set; }
        public string? BSSID { get; set; }
        public string? Signal { get; set; }
        public string? Percentage { get; set; }
        public string? Min { get; set; }
        public string? Max { get; set; }
        public string? Average { get; set; }
        public string? Level { get; set; }
        public string? Band { get; set; }
        public string? Channel { get; set; }
        public string? Width { get; set; }
        public string? Security { get; set; }
        public string? Mode { get; set; }
    }
}
