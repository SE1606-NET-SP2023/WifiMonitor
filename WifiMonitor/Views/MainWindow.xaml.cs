﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WifiMonitor.Models;
using WifiMonitor.ViewModels;
using WifiMonitor.Utils;
using System.Windows.Threading;
using WifiMonitor.Views;

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
        public static NetworkWatcher NetworkWatcherControl = new NetworkWatcher();

        public MainWindow()
        {
            InitializeComponent();
            lv.ItemsSource = AccessPointUtils.AvailableWifi;
            AccessPointUtils.OnScanSuccess += UpdateData;
            NetworkWatcherContainer.Content = NetworkWatcherControl;
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
            WifiVM.ScanInterval = interval;
            ScanTimer.Interval = new TimeSpan(0,0,interval);
        }

        void DoScan(object sender, EventArgs e)
        {
            //OnNotify.Invoke();
            utils.Scan();
        }

        private void lv_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic selectedItem = lv.SelectedItem;
            string TargetBssid = selectedItem.BSSID;
            Window container = new Window();
            TabularData1 ta = new TabularData1(TargetBssid);
            container.Title = selectedItem.SSID;
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
