using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ManagedNativeWifi;

namespace WifiMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int min = 0, max = 0;

        public MainWindow()
        {
            InitializeComponent();
            lv.ItemsSource = GetAllAvailableWifi();
        }



        public static List<WifiInformation> GetAllAvailableWifi()
        {
            List<WifiInformation> wifiList = new List<WifiInformation>();
            IEnumerable<AvailableNetworkGroupPack> networkList = NativeWifi.EnumerateAvailableNetworkGroups();

            foreach (AvailableNetworkGroupPack network in networkList)
            {
                BssNetworkPack networkPack = network.BssNetworks.ToList()[0];

                wifiList.Add(new WifiInformation
                {
                    SSID = networkPack.Ssid.ToString(),
                    BSSID = networkPack.Bssid.ToString(),
                    Signal = networkPack.SignalStrength.ToString(),
                    Band = networkPack.Band.ToString(),
                    Channel = networkPack.Channel.ToString(),
                    Security = network.AuthenticationAlgorithm.ToString().Replace("RSNA", "WPA2"),
                    Mode = network.PhyType.ToProtocolName().ToString()
                });
            }
            return wifiList;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void lv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

    }

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
