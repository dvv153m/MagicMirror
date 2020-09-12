﻿using System.Linq;
using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Models.Bluetooth;
using MagicMirror.Models.WiFi;
using MagicMirror.Views.WiFiSetupWizard;
using System.Collections.ObjectModel;
using MagicMirror.Services;

namespace MagicMirror.ViewModels.WiFiSetupWizard
{
    public class WiFiSetupNetworkPageViewModel : ViewModelBase
    {
        private INavigationPage _navigation;
        private BluetoothService _bluetoothService;
        private DataContext _mMContext;

        public WiFiSetupNetworkPageViewModel(DataContext mMContext, BluetoothService bluetoothService, INavigationPage navigation)
        {
            _mMContext = mMContext;
            _bluetoothService = bluetoothService;
            _navigation = navigation;
        }

        public AsyncCommand OnLoadedCommand => new AsyncCommand(async () =>
        {
            IsBusy = true;
            try
            {
                var bluetoothClient = new BluetoothModel();
                WiFiNetworkResponse wifiResponse = await bluetoothClient.GetNetworksAsync(_mMContext.Device);
                if (wifiResponse.IsSuccess)
                {
                    HasNetworks = wifiResponse.Networks.Any();
                    if (HasNetworks)
                    {
                        Networks = new ObservableCollection<string>(wifiResponse.Networks);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Not determine wifi networks. Please try again", "OK");
                        //add button try again get networks
                    }
                }
                else
                {
                    if (wifiResponse.ErrorCode == 2)
                    {
                        //todo вручную вводить имя сети и пароль и потом перезапустить зеркало
                    }
                    else
                    {
                        //display this error wifiResponse.ErrorInfo                                                              
                        await App.Current.MainPage.DisplayAlert("Alert", "Something went wrong, please reconnect", "OK");                        
                        await _bluetoothService.DisconnectAsync(_mMContext.Device);
                        _navigation.NextPage(new SearchingDevicePage());
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        });

        public AsyncCommand NextCommand => new AsyncCommand(async () => {

            _mMContext.SelectedNetwork = SelectedNetwork;
            _navigation.NextPage(new WifiSetupPasswordPage());

        }, () => !IsBusy);

        private ObservableCollection<string> _networks { get; set; }
        public ObservableCollection<string> Networks
        {
            get { return _networks; }
            set
            {
                _networks = value;
                OnPropertyChanged();
            }
        }

        private string _selectedNetwork { get; set; }
        public string SelectedNetwork
        {
            get { return _selectedNetwork; }
            set
            {
                _selectedNetwork = value;
                OnPropertyChanged();
                OnPropertyChanged("CanExecuteNextBtn");
            }
        }

        public bool CanExecuteNextBtn
        {
            get { return SelectedNetwork != null; }
        }

        private bool _hasNetworks { get; set; }
        public bool HasNetworks
        {
            get { return _hasNetworks; }
            set
            {
                _hasNetworks = value;
                OnPropertyChanged();
            }
        }
    }
}