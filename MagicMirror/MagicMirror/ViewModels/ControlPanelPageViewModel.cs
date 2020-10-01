using MagicMirror.Common.MVVM;
using MagicMirror.Repository;
using System.Linq;

namespace MagicMirror.ViewModels
{
    public class ControlPanelPageViewModel : ViewModelBase
    {
        //public string Url => "http://192.168.xxx.xxx:8080/remote.html"
        public string Url { get; set; }
        MagicMirrorRepository _magicMirrorRepository;

        public ControlPanelPageViewModel(MagicMirrorRepository magicMirrorRepository)
        {
            _magicMirrorRepository = magicMirrorRepository;
            var mirrors = _magicMirrorRepository.GetAll();
            if (mirrors != null && mirrors.Any())
            {
                Url = $"http://{mirrors[0].Ip}:8080/remote.html";
            }            
        }

        public AsyncCommand TestCommand => new AsyncCommand(async () =>
        {
            /*var myValue = Preferences.Get("12345", "");
            if (string.IsNullOrEmpty(myValue))
            {
                Preferences.Set("12345", "my_value12");//
            }
            else
            {
                Preferences.Set("12345", "3434");
            }*/
        });

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
