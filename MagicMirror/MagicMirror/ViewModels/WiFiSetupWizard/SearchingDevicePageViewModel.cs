using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Services;
using MagicMirror.Views.WiFiSetupWizard;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;
using MagicMirror.Common;

namespace MagicMirror.ViewModels.WiFiSetupWizard
{
    public class SearchingDevicePageViewModel : ViewModelBase
    {
        private BluetoothService _bluetoothService;
        private INavigationService _navigation { get; set; }

        private DataContext _mMContext;

        public SearchingDevicePageViewModel(DataContext mMContext, BluetoothService bluetoothService, INavigationService navigation)
        {
            _mMContext = mMContext;
            _bluetoothService = bluetoothService;
            _navigation = navigation;

            Devices = new ObservableCollection<IDevice>();
            IsBusy = false;
            Subscribe();
        }

        private ObservableCollection<IDevice> _devices { get; set; }
        public ObservableCollection<IDevice> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand FindMagicMirrorCommand => new AsyncCommand(async () =>
        {
            try
            {
                IsBusy = true;
                SelectedDevice = null;
                //await _adapter.StopScanningForDevicesAsync();//todo раскоментить при повторном поиске устройства будут ли повторения
                OnPropertyChanged("CanExecuteNextBtn");
                Devices.Clear();
                bool hasLocationStatus = await Utils.CheckPermissions(Permission.Location);
                if (!hasLocationStatus)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "App need location permission", "OK");
                    IsBusy = false;
                    return;
                }
                if (!Utils.IsBluetoothEnable())
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Alert", "Bluetooth disable", "OK");
                }
                else
                {
                    await _bluetoothService.StartScanAsync();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Error", "OK");
            }
        }, () => !IsBusy);

        public AsyncCommand NextCommand => new AsyncCommand(async () => {

            _mMContext.Device = SelectedDevice;
            _navigation.NextPage(typeof(DevicePage));            

        }, () => SelectedDevice != null);

        private IDevice _selectedDevice;
        public IDevice SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanExecuteNextBtn));
                //NextCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanExecuteNextBtn
        {
            get { return SelectedDevice != null; }
        }

        private void Subscribe()
        {
            MessagingCenter.Subscribe<BluetoothService, IDevice>(this, Constants.AddDeviceTopic, (s, device) =>
            {
                if (!string.IsNullOrEmpty(device.Name))
                {
                    Devices.Add(device);
                }
            });

            MessagingCenter.Subscribe<BluetoothService>(this, Constants.ScanStoppedTopic, (s) =>
            {
                IsBusy = false;
                OnPropertyChanged("CanExecuteNextBtn");
            });
        }
    }
}
