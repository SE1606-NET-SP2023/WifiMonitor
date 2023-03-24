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
        public TabularData1()
        {
            InitializeComponent();
            data = AccessPointUtils.ScanHistory.Where(x=>x.BSSID.Equals("A4:85:7A:B8:77:60")).ToList();
            lv.ItemsSource = data;
            utils.OnScanSuccess += UpdateData;
        }
        void UpdateData()
        {
            data.Add(AccessPointUtils.ScanHistory.Where(x => x.BSSID.Equals("A4:85:7A:B8:77:60")).Last());
            lv.Items.Refresh();
        }
    }
}
