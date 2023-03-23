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

namespace WifiMonitor.Views
{
    /// <summary>
    /// Interaction logic for TabularData.xaml
    /// </summary>
    public partial class TabularData : UserControl
    {
        public TabularData()
        {
            InitializeComponent();
            lv.ItemsSource = TabularDataView.list;
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
