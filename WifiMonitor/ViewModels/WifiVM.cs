﻿using System.Collections.Generic;
using System.Linq;
using ManagedNativeWifi;
using WifiMonitor.Models;

namespace WifiMonitor.ViewModels
{
    public class WifiVM
    {
        public static int ScanInterval = 5;
        public static List<WifiInformation> GetAllAvailableWifi()
        {
            List<WifiInformation> wifiList = new List<WifiInformation>();
            IEnumerable<AvailableNetworkGroupPack> networkList = NativeWifi.EnumerateAvailableNetworkGroups();

            foreach (AvailableNetworkGroupPack network in networkList)
            {
                BssNetworkPack networkPack = network.BssNetworks.ToList().FirstOrDefault();
                wifiList.Add(new WifiInformation
                {
                    SSID = networkPack.Ssid.ToString(),
                    BSSID = networkPack.Bssid.ToString(),
                    Signal = networkPack.SignalStrength.ToString(),
                    Band = networkPack.Band.ToString(),
                    Channel = networkPack.Channel.ToString(),
                    Security = network.AuthenticationAlgorithm.ToString().Replace("RSNA", "WPA2"),
                    Mode = network.PhyType.ToProtocolName().ToString()
                });
            }
            return wifiList.DistinctBy(x => x.BSSID).ToList();
        }
    }
}
