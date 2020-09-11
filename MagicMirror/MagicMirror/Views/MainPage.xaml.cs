using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using MagicMirror.ViewModels;

namespace MagicMirror.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {        
        public MainPage()
        {
            Detail = new NavigationPage(new ControlPanelPage());
            InitializeComponent();            
            //BindingContext = new MainPageViewModel();
        }        
    }
}