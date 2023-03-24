using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiMonitor.Models;
using WifiMonitor.ViewModels;

namespace WifiMonitor.Utils
{
    public class AccessPointUtils
    {
        public delegate void ScanNotify();
        public event ScanNotify? OnScanSuccess;

        public static List<WifiInformation> AvailableWifi = new List<WifiInformation>();
        public static List<AccessPointSnapshot> ScanHistory = new List<AccessPointSnapshot>();

        public AccessPointUtils()
        {          
        }
        public AccessPointUtils(MainWindow window)
        {
            window.OnNotify += Scan;
        }

        public void Scan()
        {
            if (WifiVM.GetAllAvailableWifi().Count != 0)
            {
                AvailableWifi = WifiVM.GetAllAvailableWifi();
                UpdateScanHistory();
                OnScanSuccess?.Invoke();
            }
        }

        void UpdateScanHistory()
        {
            TimeOnly RecordTime = TimeOnly.FromDateTime(DateTime.Now);
            foreach(WifiInformation wf in AvailableWifi)
            {
                ScanHistory.Add(new AccessPointSnapshot
                {
                    SSID = wf.SSID,
                    BSSID = wf.BSSID,
                    Signal = wf.Signal,
                    Band = wf.Band,
                    Channel = wf.Channel,
                    Width = wf.Width,
                    Security = wf.Security,
                    Mode = wf.Mode,
                    DetectedTime = RecordTime
                });
            }
        }
        
    }
}
