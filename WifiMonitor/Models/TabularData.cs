using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiMonitor.Models
{
    public class TabularData
    {
        public TimeOnly? Time { get; set; }
        public string? Signal { get; set; }
        public string? Channel { get; set; }
        public string? SecurityMode { get; set; }

    }
}
