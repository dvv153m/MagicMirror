

namespace MagicMirror.Models.WiFi
{
    public class WiFiNetworksResponse
    {
        public string[] Networks { get; set; }

        public bool IsSuccess { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorInfo { get; set; }
    }
}
