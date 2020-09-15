using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MagicMirror.ViewModels
{
    public class ControlPanelPageViewModel : ViewModelBase
    {
        //public string Url => "http://192.168.xxx.xxx:8080/remote.html"
        public string Url => "https://yandex.ru";

        public ControlPanelPageViewModel()
        {
            //Preferences.Set("my_key", "my_value");
            //var myValue = Preferences.Get("my_key", "default_value");
        }

        public AsyncCommand TestCommand => new AsyncCommand(async() =>
        {
            await Task.Delay(1000);
            int f = 5 + 5;
        });

    }
}
