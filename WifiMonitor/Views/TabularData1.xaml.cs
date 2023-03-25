using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
using Microsoft.Win32;
using ClosedXML.Excel;

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

        private string Signal { get; set; } = "-100";
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

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "File name",
                DefaultExt = ".xlsx",
                Filter = "Excel (.xlsx)|* .xlsx"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
            }
                
            if (!string.IsNullOrEmpty(path))
            {
                bool exported = Export<AccessPointSnapshot>(data, path, "Data");
                if (exported)
                {
                    MessageBox.Show("Exported success");
                }
                else
                {
                    MessageBox.Show("Can not export");
                }
            }
        }

        private bool Export<T>(List<T> list, string file, string sheetName)
        {
            bool export = false;
            using (IXLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(sheetName).FirstCell().InsertTable<T>(list, false);
                workbook.SaveAs(file);
                export = true;
            }
            return export;
        }
    }
    public class MeasureModel
    {
        public double ElapsedMilliseconds { get; set; }
        public double Value { get; set; }
    }
}
