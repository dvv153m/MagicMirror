using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Models.Bluetooth;
using MagicMirror.Models.WiFi;
using MagicMirror.Services;
using MagicMirror.Views.WiFiSetupWizard;


namespace MagicMirror.ViewModels.WiFiSetupWizard
{
    public class WiFiSetupCredentialsPageViewModel : ViewModelBase
    {
        private INavigationService _navigation;
        private BluetoothService _bluetoothService;
        private DataContext _mMContext;

        public WiFiSetupCredentialsPageViewModel(DataContext mMContext, BluetoothService bluetoothService, INavigationService navigation)
        {
            _mMContext = mMContext;
            _bluetoothService = bluetoothService;
            _navigation = navigation;
        }

        public AsyncCommand SetCredentialsCommand => new AsyncCommand(async () =>
        {
            try
            {
                IsBusy = true;
                var wiFiCredentialsRequest = new WiFiCredentialsRequest { Ssid = Ssid, Password = Password };
                //var wiFiCredentialsRequest = new WiFiCredentialsRequest { Ssid = "SCOUT_CORP", Password = "IntelSoft2033" };
                var bluetoothClient = new BluetoothModel();
                WiFiCredentialsResponse response = await bluetoothClient.SetWifiCredentialsAsync(_mMContext.Device, wiFiCredentialsRequest);
                if (response.IsSuccess)
                {
                    _mMContext.Result = true;
                    _mMContext.MagicMiror.Ip = response.Ip;
                    _mMContext.MagicMiror.BleAddress = response.BleAddress;
                }
                else
                {
                    //todo разобрать ошибки из response.ErrorCode
                    //добавить кнопку подробно если есть описание эксепшена
                    _mMContext.Result = false;
                    _mMContext.ErrorInfo = "Failed to connect to wifi";
                }
            }
            finally
            {
                IsBusy = false;
                _navigation.NextPage(typeof(FinishPage));
            }
        });

        private string _ssid;
        public string Ssid
        {
            get { return _ssid; }
            set
            {
                _ssid = value;
                OnPropertyChanged();                
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();                
            }
        }
    }
}
