using ManagedNativeWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiMonitor.Models;

namespace WifiMonitor.ViewModels
{
    class TabularDataView
    {
        public static List<TabularData> list = GetALLTabulaData();
        //public static List<TabularData> GetALLTabulaData()
        //{
        //    var tabulardata = new TabularData[]
        //    {
        //        new TabularData{
        //        Time = new TimeOnly(12, 5, 6),
        //        Signal = "-76",
        //        Channel = "124",
        //        SecurityMode = "WP2 Personal" },
        //        new TabularData{
        //        Time = new TimeOnly(1, 53, 1),
        //        Signal = "-76",
        //        Channel = "124",
        //        SecurityMode = "WP2 Personal"},
        //        new TabularData
        //        {
        //        Time = new TimeOnly(12, 5, 6),
        //        Signal = "-76",
        //        Channel = "124",
        //        SecurityMode = "WP2 Personal"
        //        },
        //        new TabularData
        //        {
        //        Time = new TimeOnly(12, 5, 6),
        //        Signal = "-76",
        //        Channel = "124",
        //        SecurityMode = "WP2 Personal"
        //        },
        //        new TabularData
        //        {
        //        Time = new TimeOnly(12, 5, 6),
        //        Signal = "-76",
        //        Channel = "124",
        //        SecurityMode = "WP2 Personal"
        //        }

        //    };

        //    List<TabularData> tdl = new List<TabularData>();
        //    tdl.AddRange(tabulardata);
        //    return tdl.ToList();
        //}
        public static List<TabularData> GetALLTabulaData()
        {
            List<TabularData> tbl = new List<TabularData>();
            IEnumerable<AvailableNetworkGroupPack> networkList = NativeWifi.EnumerateAvailableNetworkGroups();

            foreach (AvailableNetworkGroupPack network in networkList)
            {
                BssNetworkPack networkPack = network.BssNetworks.ToList()[0];

                tbl.Add(new TabularData
                {
                    Time = new TimeOnly (2,32,1),
                    Signal = networkPack.SignalStrength.ToString(),
                    Channel = networkPack.Channel.ToString(),
                    SecurityMode = network.AuthenticationAlgorithm.ToString().Replace("RSNA", "WPA2")
                });
            }
            return tbl.ToList();
        }

    }

}
