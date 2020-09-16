using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Services;
using MagicMirror.Views;


namespace MagicMirror.ViewModels.WiFiSetupWizard
{
    public class FinishPageViewModel : ViewModelBase
    {
        private INavigationService _navigation;
        private BluetoothService _bluetoothService;
        private DataContext _mMContext;        

        public FinishPageViewModel(DataContext mMContext, BluetoothService bluetoothService, INavigationService navigation)
        {
            _mMContext = mMContext;
            _bluetoothService = bluetoothService;
            _navigation = navigation;

            if (_mMContext.Result)
            {
                Result = "Success connection";
            }
            else
            {
                Result = _mMContext.ErrorInfo;
            }
        }

        public AsyncCommand GoToMainViewCommand => new AsyncCommand(async () => {
            
            await _bluetoothService.DisconnectAsync(_mMContext.Device);
            _navigation.NextPage(typeof(ControlPanelPage));

        });

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }
    }
}
