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
using LiveCharts;
using System.Diagnostics;
using LiveCharts.Wpf;
using LiveCharts.Definitions.Series;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using LiveCharts.Helpers;
using LiveCharts.Configurations;
using WifiMonitor.ViewModels;

namespace WifiMonitor.Views
{
    /// <summary>
    /// Interaction logic for TabularData1.xaml
    /// </summary>
    public partial class TabularData1 : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SeriesCollection Series { get; set; } = new SeriesCollection();

        public List<ISeriesView> SeriesLines { get; set; } = new List<ISeriesView>();

        List<AccessPointSnapshot> data = new List<AccessPointSnapshot>();

        private string Signal { get; set; } = "0";
        string TargetBSSID = "";
        public TabularData1(string TargetBssid)
        {
            InitializeComponent();
            this.TargetBSSID = TargetBssid;
            AccessPointUtils.OnScanSuccess += UpdateData;
            //lets instead plot elapsed milliseconds and value
            var mapper = Mappers.Xy<MeasureModel>()
                .X(x => x.ElapsedMilliseconds)
                .Y(x => x.Value);

            //save the mapper globally         
            Charting.For<MeasureModel>(mapper);

            SeriesLines.Add(new LineSeries { Values = new ChartValues<MeasureModel>() });
            var sw = new Stopwatch();
            sw.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    SeriesLines.ForEach(x =>
                    {
                        if (x.Values.Count > 10)
                        {
                            x.Values.RemoveAt(0);
                        }
                        x.Values.Add(new MeasureModel
                        {
                            ElapsedMilliseconds = sw.ElapsedMilliseconds,
                            Value = double.Parse(Signal) * -1
                        });
                    });
                    Thread.Sleep(WifiVM.ScanInterval * 1000);
                }
            });
            Series = SeriesLines.AsSeriesCollection();
            DataContext = this;
        }
        void UpdateData()
        {
            data = AccessPointUtils.ScanHistory.Where(x => x.BSSID.Equals(TargetBSSID)).Reverse().ToList();
            lv.ItemsSource = null;
            lv.ItemsSource = data;
            Signal = data.First().Signal;
        }

    }
    public class MeasureModel
    {
        public double ElapsedMilliseconds { get; set; }
        public double Value { get; set; }
    }
}
