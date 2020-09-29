

namespace MagicMirror.Models.WiFi
{
    public class WiFiCredentialsResponse
    {
        public string Ip { get; set; }

        public string BleAddress  { get; set; }

        public bool IsSuccess { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorInfo { get; set; }
    }
}
