using Xamarin.Forms;
using MagicMirror.IoC;
using MagicMirror.Models;
using MagicMirror.Views;
using MagicMirror.Common.MVVM;
using System.Collections.Generic;
using MagicMirror.Views.WiFiSetupWizard;
using Microsoft.Extensions.DependencyInjection;


namespace MagicMirror.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public List<MainMenuItem> MainMenuItems { get; set; }        

        public MainPageViewModel()
        {            
            MainMenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "Main", Icon = "menu_inbox.png", TargetType = typeof(ControlPanelPage) },
                new MainMenuItem() { Title = "Settings", Icon = "menu_stock.png", TargetType = typeof(SearchingDevicePage) }
            };            
        }

        private MainMenuItem _selectedMenuItem;
        public MainMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                if (_selectedMenuItem != null)
                {                        
                    var page = (Page)ViewModelLocator.ServiceProvider.GetRequiredService(_selectedMenuItem.TargetType);
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(page);                    
                    (App.Current.MainPage as MasterDetailPage).IsPresented = false;
                }
                _selectedMenuItem = null;
                OnPropertyChanged();
            }
        }
    }
}
