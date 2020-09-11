using System;
using System.Collections.Generic;
using System.Text;

namespace MagicMirror.Models
{
    public static class Constants
    {
        public static readonly Guid WifiServiceGuid = new Guid("C7841029FE7C48948532F97908EF1AE4");

        public static readonly Guid WiFiNetworksCharacteristicsGuid = new Guid("fffffffffffffffffffffffffffffff2");

        public static readonly Guid WiFiCredentialsCharacteristicsGuid = new Guid("fffffffffffffffffffffffffffffff3");

        public static string AddDeviceTopic = "AddDevice";
        public static string ScanStoppedTopic = "ScanStopped";
    }
}
