using System.ComponentModel;
using Xamarin.Forms;
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
            var page = ViewModelLocator.ServiceProvider.GetRequiredService<MagicMirrorsPage>();            
            Detail = new NavigationPage(page);
            //var controlPanelPage = ViewModelLocator.ServiceProvider.GetRequiredService<ControlPanelPage>();            
            //Detail = new NavigationPage(controlPanelPage);
            InitializeComponent();                        
        }        
    }
}