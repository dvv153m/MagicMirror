using Plugin.BLE.Abstractions.Contracts;


namespace MagicMirror.Models.WiFi
{
    public class WiFiResponse
    {
        public ICharacteristic Characteristic { get; set; }

        public bool IsSuccess { get; set; }

        public int ErrorCode { get; set; }
    }
}
