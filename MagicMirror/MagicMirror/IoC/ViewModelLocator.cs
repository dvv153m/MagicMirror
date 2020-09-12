using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Services;
using MagicMirror.ViewModels;
using MagicMirror.ViewModels.WiFiSetupWizard;
using MagicMirror.Views;
using MagicMirror.Views.WiFiSetupWizard;
using Microsoft.Extensions.DependencyInjection;

namespace MagicMirror.IoC
{
    public class ViewModelLocator
    {
        private static ServiceProvider _serviceProvider;

        public static ServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        public static void Init()
        {
            var services = new ServiceCollection();            
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<ControlPanelPageViewModel>();
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

            services.AddSingleton<ControlPanelPage>();
            services.AddSingleton<SearchingDevicePage>();
            

            _serviceProvider = services.BuildServiceProvider();

            /*foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }*/
        }

        public MainPageViewModel MainPageViewModel => _serviceProvider.GetRequiredService<MainPageViewModel>();

        public ControlPanelPageViewModel ControlPanelPageViewModel => _serviceProvider.GetRequiredService<ControlPanelPageViewModel>();        
        
        public SearchingDevicePageViewModel SearchingDevicePageViewModel => _serviceProvider.GetRequiredService<SearchingDevicePageViewModel>();

        public DevicePageViewModel DevicePageViewModel => _serviceProvider.GetRequiredService<DevicePageViewModel>();

        public WiFiSetupNetworkPageViewModel WiFiSetupNetworkPageViewModel => _serviceProvider.GetRequiredService<WiFiSetupNetworkPageViewModel>();

        public WifiSetupPasswordPageViewModel WifiSetupPasswordPageViewModel => _serviceProvider.GetRequiredService<WifiSetupPasswordPageViewModel>();

        public FinishPageViewModel FinishPageViewModel => _serviceProvider.GetRequiredService<FinishPageViewModel>();

        public DataContext MMContext => _serviceProvider.GetRequiredService<DataContext>();
    }
}
