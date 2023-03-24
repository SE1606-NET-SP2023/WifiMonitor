using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WifiMonitor.Models;
using WifiMonitor.ViewModels;
using WifiMonitor.Utils;
using System.Windows.Threading;

namespace WifiMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ActionNotify();
        public event ActionNotify? OnNotify;
        private int min = 0, max = 0;
        private Random random = new Random();
        AccessPointUtils utils = new AccessPointUtils();
        DispatcherTimer ScanTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            lv.ItemsSource = AccessPointUtils.AvailableWifi;
            AccessPointUtils.OnScanSuccess += UpdateData;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ScanTimer.Tick += new EventHandler(DoScan);
            ScanTimer.Interval = new TimeSpan(0, 0, 5);
            ScanTimer.Start();
        }

        private void cbInterval_OnChange(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbInterval.SelectedItem;
            int interval = Convert.ToInt32(item.Content.ToString().Replace(" seconds",""));
            ScanTimer.Interval = new TimeSpan(0,0,interval);
        }

        void DoScan(object sender, EventArgs e)
        {
            //OnNotify.Invoke();
            utils.Scan();
        }

        //private void CheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //    Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));


        //    CheckBox currentCheckbox = (sender as CheckBox)!;
        //    string currentBSSID = (currentCheckbox.Content as TextBlock)!.Text;


        //    WifiInformation selectedWifi = AccessPointUtils.AvailableWifi.Where(x => x.BSSID!.Equals(currentBSSID)).FirstOrDefault()!;

        //    selectedWifi.Color = "#" + randomColor.R.ToString("X2") + randomColor.G.ToString("X2") + randomColor.B.ToString("X2");

        //    lv.ItemsSource = null;
        //    lv.ItemsSource = AccessPointUtils.AvailableWifi;
        //}

        //private void lv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{

        //}

        void UpdateData()
        {
            lv.ItemsSource = null;
            lv.ItemsSource = AccessPointUtils.AvailableWifi;
        }

    }
}
