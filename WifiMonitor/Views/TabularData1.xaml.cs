using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WifiMonitor.Utils;
using WifiMonitor.Models;

namespace WifiMonitor.Views
{
    /// <summary>
    /// Interaction logic for TabularData1.xaml
    /// </summary>
    public partial class TabularData1 : UserControl
    {
        List<AccessPointSnapshot> data = new List<AccessPointSnapshot>();
        AccessPointUtils utils = new AccessPointUtils();
        string TargetBSSID = "";
        public TabularData1(string TargetBssid)
        {
            InitializeComponent();
            this.TargetBSSID = TargetBssid;
            AccessPointUtils.OnScanSuccess += UpdateData;
        }
        void UpdateData()
        {
            lv.ItemsSource = null;
            lv.ItemsSource = AccessPointUtils.ScanHistory.Where(x => x.BSSID.Equals(TargetBSSID)).Reverse();
        }
    }
}
