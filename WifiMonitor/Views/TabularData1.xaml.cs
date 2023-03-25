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
using LiveCharts.Definitions.Series;
using LiveCharts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System.Diagnostics;
using System.Threading;

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

        public TabularData1()
        {
            InitializeComponent();
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
                    Thread.Sleep(500);
                }
            });
            Series = SeriesLines.AsSeriesCollection();
            DataContext = this;
        }

        void UpdateData()
        {
            data.Add(AccessPointUtils.ScanHistory.Where(x => x.BSSID.Equals("C8:E7:D8:95:1E:6C")).Last());
            lv.Items.Refresh();
            lv.ItemsSource = data;
            Signal = data.Last().Signal;
        }
    }

    public class MeasureModel
    {
        public double ElapsedMilliseconds { get; set; }
        public double Value { get; set; }
    }
}
