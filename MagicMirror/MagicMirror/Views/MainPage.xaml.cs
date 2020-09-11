using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using MagicMirror.ViewModels;
using MagicMirror.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace MagicMirror.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {        
        public MainPage()
        {
            var controlPanelPage = ViewModelLocator.ServiceProvider.GetRequiredService<ControlPanelPage>();
            Detail = new NavigationPage(controlPanelPage);
            InitializeComponent();            
            //BindingContext = new MainPageViewModel();
        }        
    }
}