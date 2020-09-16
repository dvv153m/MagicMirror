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
                    IsVisibleSuccessConnection = true;
                    CanExecuteNextBtn = true;
                }
                else
                {

                    IsVisibleNotConnection = true;
                }
            }
            catch (DeviceConnectionException e)
            {
                //todo add display info
                // ... could not connect to device
                IsVisibleNotConnection = true;
            }
            catch (Exception ex)
            {
                //generic
                IsVisibleNotConnection = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public AsyncCommand NextCommand => new AsyncCommand(async () => {
            
            _navigation.NextPage(typeof(WiFiSetupNetworkPage));

        }, () => IsVisibleSuccessConnection && !IsBusy);

        private bool _isVisibleSuccessConnection { get; set; }
        public bool IsVisibleSuccessConnection
        {
            get { return _isVisibleSuccessConnection; }
            set
            {
                _isVisibleSuccessConnection = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisibleNotConnection { get; set; }
        public bool IsVisibleNotConnection
        {
            get { return _isVisibleNotConnection; }
            set
            {
                _isVisibleNotConnection = value;
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
