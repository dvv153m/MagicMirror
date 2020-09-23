using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Repository;
using MagicMirror.Services;
using MagicMirror.ViewModels;
using MagicMirror.ViewModels.WiFiSetupWizard;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

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
            
            //регистрируем viewModel
            Assembly.GetExecutingAssembly().DefinedTypes
                 .Where(type => type.IsClass)
                 .Where(type => type.Name.EndsWith("PageViewModel"))
                 .ToList()
                 .ForEach(viewModelType => services.AddTransient(viewModelType));

            //регистрируем view(page) //info если регистрируем как AddSingleton то не регистрирует если как AddTransient то норм
            Assembly.GetExecutingAssembly().DefinedTypes
                 .Where(type => type.IsClass)
                 .Where(type => type.Name.EndsWith("Page"))
                 .ToList()
                 .ForEach(viewType => services.AddTransient(viewType));

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<DataContext>();
            services.AddSingleton<BluetoothService>();
            services.AddSingleton<MagicMirrorRepository>();

            _serviceProvider = services.BuildServiceProvider();

            /*foreach (var item in services)
            {
                if (item.ServiceType.Name == "MagicMirrorsPage")
                    continue;
                _serviceProvider.GetRequiredService(item.ServiceType);
            }*/
        }

        public MainPageViewModel MainPageViewModel => _serviceProvider.GetRequiredService<MainPageViewModel>();

        public MagicMirrorsPageViewModel MagicMirrorsPageViewModel => _serviceProvider.GetRequiredService<MagicMirrorsPageViewModel>();        

        public ControlPanelPageViewModel ControlPanelPageViewModel => _serviceProvider.GetRequiredService<ControlPanelPageViewModel>();        
        
        public SearchingDevicePageViewModel SearchingDevicePageViewModel => _serviceProvider.GetRequiredService<SearchingDevicePageViewModel>();

        public DevicePageViewModel DevicePageViewModel => _serviceProvider.GetRequiredService<DevicePageViewModel>();

        public WiFiSetupNetworkPageViewModel WiFiSetupNetworkPageViewModel => _serviceProvider.GetRequiredService<WiFiSetupNetworkPageViewModel>();

        public WifiSetupPasswordPageViewModel WifiSetupPasswordPageViewModel => _serviceProvider.GetRequiredService<WifiSetupPasswordPageViewModel>();

        public FinishPageViewModel FinishPageViewModel => _serviceProvider.GetRequiredService<FinishPageViewModel>();

        public DataContext MMContext => _serviceProvider.GetRequiredService<DataContext>();
    }
}
