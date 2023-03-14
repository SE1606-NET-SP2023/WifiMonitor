using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WifiMonitor.Models;
using WifiMonitor.ViewModels;

namespace WifiMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        private int min = 0, max = 0;

        public MainWindow()
        {
            InitializeComponent();
            lv.ItemsSource = WifiMonitorHelper.list;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));


            CheckBox currentCheckbox = (sender as CheckBox)!;
            // currentCheckbox.Background = (randomColor as Brush);
            string currentBSSID = (currentCheckbox.Content as TextBlock)!.Text;

            ItemCollection items = lv.Items;



            WifiInformation selectedWifi = WifiMonitorHelper.list.Where(x => x.BSSID!.Equals(currentBSSID)).FirstOrDefault()!;

            selectedWifi.Color = "#" + randomColor.R.ToString("X2") + randomColor.G.ToString("X2") + randomColor.B.ToString("X2");

            // MessageBox.Show($"{selectedWifi.SSID}");
            lv.ItemsSource = null;
            lv.ItemsSource = WifiMonitorHelper.list;
        }

        private void lv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

    }
}
