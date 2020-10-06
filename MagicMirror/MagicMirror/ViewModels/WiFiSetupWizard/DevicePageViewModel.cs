using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Services;
using MagicMirror.Views.WiFiSetupWizard;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MagicMirror.ViewModels.WiFiSetupWizard
{
    public class DevicePageViewModel : ViewModelBase
    {
        private BluetoothService _bluetoothService;

        private INavigationService _navigation;

        private DataContext _mMContext;

        public DevicePageViewModel(DataContext mMContext, BluetoothService bluetoothService, INavigationService navigation)
        {
            _navigation = navigation;
            _mMContext = mMContext;
            _bluetoothService = bluetoothService;
        }

        public AsyncCommand OnLoadedCommand => new AsyncCommand(async () => 
        {
            await ConnectingToDeviceAsync();
        });

        private async Task ConnectingToDeviceAsync()
        {            
            try
            {
                IsBusy = true;

                await _bluetoothService.ConnectAsync(_mMContext.Device);
                if (_mMContext.Device.State == Plugin.BLE.Abstractions.DeviceState.Connected)
                {
                    IsSuccessConnection = true;
                    CanExecuteNextBtn = true;
                    _navigation.NextPage(typeof(WiFiSetupNetworkPage));
                }
                else
                {

                    IsNotSuccessConnection = true;
                }
            }
            catch (DeviceConnectionException e)
            {
                //todo add display info
                // ... could not connect to device
                IsNotSuccessConnection = true;
            }
            catch (Exception ex)
            {
                //generic
                IsNotSuccessConnection = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public AsyncCommand TryAgainCommand => new AsyncCommand(async () => {

            await ConnectingToDeviceAsync();

        }, () => IsNotSuccessConnection && !IsBusy);


        private bool _isSuccessConnection { get; set; }
        public bool IsSuccessConnection
        {
            get { return _isSuccessConnection; }
            set
            {
                _isSuccessConnection = value;
                OnPropertyChanged();
            }
        }

        private bool _isNotSuccessConnection { get; set; }
        public bool IsNotSuccessConnection
        {
            get { return _isNotSuccessConnection; }
            set
            {
                _isNotSuccessConnection = value;
                OnPropertyChanged();
            }
        }

        private bool _canExecuteNextBtn;
        public bool CanExecuteNextBtn
        {
            get { return _canExecuteNextBtn; }
            set
            {
                _canExecuteNextBtn = value;
                OnPropertyChanged();
            }
        }
    }
}
