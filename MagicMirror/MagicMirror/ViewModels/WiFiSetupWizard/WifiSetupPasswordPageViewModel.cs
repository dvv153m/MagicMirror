using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Models.Bluetooth;
using MagicMirror.Models.WiFi;
using MagicMirror.Views.WiFiSetupWizard;


namespace MagicMirror.ViewModels.WiFiSetupWizard
{
    public class WifiSetupPasswordPageViewModel : ViewModelBase
    {
        private INavigationService _navigation;
        private DataContext _mMContext;

        public WifiSetupPasswordPageViewModel(DataContext mMContext, INavigationService navigation)
        {
            _mMContext = mMContext;
            _navigation = navigation;
            NetworkName = _mMContext.SelectedNetwork;
        }

        public AsyncCommand ConnectCommand => new AsyncCommand(async () => {

            try
            {
                IsBusy = true;
                OnPropertyChanged("CanExecuteConnect");

                var wiFiCredentialsRequest = new WiFiCredentialsRequest { Ssid = _mMContext.SelectedNetwork, Password = Password };
                var bluetoothClient = new BluetoothModel();
                WiFiCredentialsResponse response = await bluetoothClient.SetWifiCredentialsAsync(_mMContext.Device, wiFiCredentialsRequest);

                if (response.IsSuccess)
                {
                    _mMContext.Result = true;
                    _mMContext.Ip = response.Ip;
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

        }, () => CanExecuteConnect);

        public string NetworkName { get; set; }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanExecuteConnect));
            }
        }

        public bool CanExecuteConnect
        {
            get { return Password?.Length > 0 && !IsBusy; }
        }
    }
}
