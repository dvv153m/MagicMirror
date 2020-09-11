using Plugin.BLE.Abstractions.Contracts;

namespace MagicMirror.Models
{
    public class DataContext
    {
        public IDevice Device { get; set; }

        public string SelectedNetwork { get; set; }

        public string Ip { get; set; }

        /// <summary>
        /// Результат подключения к wifi
        /// </summary>
        public bool Result { get; set; }

        public string ErrorInfo { get; set; }
    }
}
