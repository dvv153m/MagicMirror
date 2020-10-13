using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Repository;
using MagicMirror.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicMirror.ViewModels
{
    public class ControlPanelPageViewModel : ViewModelBase
    {
        //public string Url => "http://192.168.xxx.xxx:8080/remote.html"
        
        public string Url
        {
            get { return _url; }
            set 
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        private string _url;

        private MagicMirrorRepository _magicMirrorRepository;
        private List<Models.MagicMirror> _magicMirrors;
        private INavigationService _navigation;

        public ControlPanelPageViewModel(MagicMirrorRepository magicMirrorRepository, INavigationService navigation)
        {
            _magicMirrorRepository = magicMirrorRepository;
            _navigation = navigation;                                    
        }

        public override Task InitializeAsync(object navigationData)
        {
            Models.MagicMirror magicMirror;
            if (navigationData is Models.MagicMirror)
            {
                _magicMirrors = _magicMirrorRepository.GetAll();
                magicMirror = (Models.MagicMirror)navigationData;
                var currentMM = _magicMirrors.Where(m => m.Ip == magicMirror.Ip).FirstOrDefault();
                if (currentMM != null)
                {
                    Url = $"http://{currentMM.Ip}:8080/remote.html";                    
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Alert", "Something went wrong, please select another mirror", "OK");
                    _navigation.NextPage<MagicMirrorsPage>();
                }
            }

            return base.InitializeAsync(navigationData);
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
