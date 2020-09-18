

namespace MagicMirror.Models.WiFi
{
    public class WiFiCredentialsResponse
    {
        public string Ip { get; set; }

        //public string MacAddress { get; set; }//todo add mac address in firmware

        public bool IsSuccess { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorInfo { get; set; }
    }
}
