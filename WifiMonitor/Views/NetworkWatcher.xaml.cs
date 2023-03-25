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
using WifiMonitor.ViewModels;
using WifiMonitor.Utils;

namespace WifiMonitor.Views
{
    /// <summary>
    /// Interaction logic for NetworkWatcher.xaml
    /// </summary>
    public partial class NetworkWatcher : UserControl
    {
        public NetworkWatcher()
        {
            InitializeComponent();
            AccessPointUtils.OnScanSuccess += UpdateData;
        }

        void UpdateData()
        {
            lvNetworkClient.ItemsSource = null;
            lvNetworkClient.ItemsSource = NetworkWatcherVM.GetNetworkClients();
        }
    }
}
