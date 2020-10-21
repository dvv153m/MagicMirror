using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicMirror.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlPanelPage : ContentPage
    {
        public ControlPanelPage()
        {
            InitializeComponent();
            //webView.Navigated += WebView_Navigated;
        }

        private void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            busyIndicator.IsRunning = true;
        }

        private void webView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            busyIndicator.IsRunning = false;
            busyIndicator.IsVisible = false;
        }

        /*private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Result != WebNavigationResult.Success)
            {
                //Handle error here!
            }
        }

        private void OnBack_Clicked(object sender, EventArgs e)
        {            
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        private void OnForward_Clicked(object sender, EventArgs e)
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        private void Reload_Clicked(object sender, EventArgs e)
        {
            webView.Reload();
        }*/
    }
}