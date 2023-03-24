using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WifiMonitor.Models;
using WifiMonitor.ViewModels;
using WifiMonitor.Utils;
using System.Windows.Threading;
using WifiMonitor.Views;
using System.ComponentModel;
using System.Reflection;

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
        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            Window container = new Window();
            TabularData1 ta = new TabularData1();

            container.Content = ta;
            container.Show();
        }

        void UpdateData()
        {
            lv.ItemsSource = null;
            lv.ItemsSource = AccessPointUtils.AvailableWifi;
        }
    }
}
