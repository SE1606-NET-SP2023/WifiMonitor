using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ManagedNativeWifi;
using WifiMonitor.Models;
using WifiMonitor.ViewModels;

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
            lv.ItemsSource = WifiMonitorHelper.GetAllAvailableWifi();
        }        

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void lv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

    }    
}
