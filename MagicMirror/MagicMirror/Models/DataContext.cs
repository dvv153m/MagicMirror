using Plugin.BLE.Abstractions.Contracts;

namespace MagicMirror.Models
{
    public class DataContext
    {
        public IDevice Device { get; set; }

        public MagicMiror MagicMiror { get; set; }

        /// <summary>
        /// Результат подключения к wifi
        /// </summary>
        public bool Result { get; set; }

        public string ErrorInfo { get; set; }

        public DataContext()
        {
            MagicMiror = new MagicMiror();
        }
    }
}
