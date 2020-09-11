﻿using MagicMirror.Common.MVVM;
using MagicMirror.Models;
using MagicMirror.Views;
using MagicMirror.Views.WiFiSetupWizard;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicMirror.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public List<MainMenuItem> MainMenuItems { get; set; }

        public MainPageViewModel()
        {
            MainMenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "Main", Icon = "menu_inbox.png" },
                new MainMenuItem() { Title = "Settings", Icon = "menu_stock.png" }
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
                    //todo по _selectedMenuItem.TargetType из IoC контейнера вытягиваем вьюху
                    if (_selectedMenuItem.Title.Equals("Main"))
                    {
                        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new ControlPanelPage());
                    }
                    else if (_selectedMenuItem.Title.Equals("Settings"))
                    {
                        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SearchingDevicePage());
                    }

                    (App.Current.MainPage as MasterDetailPage).IsPresented = false;
                }
            }
        }
    }
}
