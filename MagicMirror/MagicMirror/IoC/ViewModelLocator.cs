﻿using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Services;
using MagicMirror.ViewModels;
using MagicMirror.ViewModels.WiFiSetupWizard;
using Microsoft.Extensions.DependencyInjection;

namespace MagicMirror.IoC
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();
            services.AddTransient<MainPageViewModel>();            
            services.AddTransient<SearchingDevicePageViewModel>();
            services.AddTransient<DevicePageViewModel>();
            services.AddTransient<WiFiSetupNetworkPageViewModel>();
            services.AddTransient<WifiSetupPasswordPageViewModel>();
            services.AddTransient<FinishPageViewModel>();

            /*GetType().Assembly.GetTypes()
                 .Where(type => type.IsClass)
                 .Where(type => type.Name.EndsWith("ViewModel"))
                 .ToList()
                 .ForEach(viewModel => services.AddTransient(viewModel));*/

            services.AddSingleton<INavigationPage, NavigationPage>();
            services.AddSingleton<DataContext>();
            services.AddSingleton<BluetoothService>();

            //services.AddSingleton<SearchingDeviceView>();

            _provider = services.BuildServiceProvider();

            /*foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }*/
        }

        public MainPageViewModel MainPageViewModel => _provider.GetRequiredService<MainPageViewModel>();

        
        //public static SearchingDeviceView SearchingDeviceView => _provider.GetRequiredService<SearchingDeviceView>();//todo можно ли view 
        public SearchingDevicePageViewModel SearchingDeviceViewModel => _provider.GetRequiredService<SearchingDevicePageViewModel>();

        public DevicePageViewModel DeviceViewModel => _provider.GetRequiredService<DevicePageViewModel>();

        public WiFiSetupNetworkPageViewModel WiFiSetupViewModel => _provider.GetRequiredService<WiFiSetupNetworkPageViewModel>();

        public WifiSetupPasswordPageViewModel WifiSetupPasswordViewModel => _provider.GetRequiredService<WifiSetupPasswordPageViewModel>();

        public FinishPageViewModel FinishViewModel => _provider.GetRequiredService<FinishPageViewModel>();

        public DataContext MMContext => _provider.GetRequiredService<DataContext>();
    }
}
