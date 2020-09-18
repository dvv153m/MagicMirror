using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Models;
using MagicMirror.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MagicMirror.ViewModels
{
    public class ControlPanelPageViewModel : ViewModelBase
    {
        //public string Url => "http://192.168.xxx.xxx:8080/remote.html"
        public string Url => "http://yy334234234.ru/";

        public ControlPanelPageViewModel()
        {
            //Preferences.Set("my_key", "my_value");
            //var myValue = Preferences.Get("my_key", "default_value");
        }


        /*public AsyncCommand NavigatingCommand => new AsyncCommand(async () =>
        {
            await Browser.OpenAsync("https://yandex.ru/", BrowserLaunchMode.SystemPreferred);
            await Task.Delay(1000);
            int f = 5 + 5;
        });*/

        /*public Command<WebNavigatingEventArgs> NavigatingCommand
        {
            get
            {
                return navigatingCommand ?? (navigatingCommand = new Command<WebNavigatingEventArgs>(
                    (param) =>
                    {
                        if (param != null && -1 < Array.IndexOf(_uris, param.Url))
                        {
                            Device.OpenUri(new Uri(param.Url));
                            param.Cancel = true;
                        }
                    }, (param) => true));
            }
        }*/

    }
}
