using MagicMirror.Common.Navigation;
using MagicMirror.IoC;
using MagicMirror.Repository;
using MagicMirror.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicMirror.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MagicMirrorsPage : ContentPage
    {
        public MagicMirrorsPage()
        {
            InitializeComponent();
            //BindingContext="{Binding MagicMirrorsPageViewModel, Source={StaticResource ViewModelLocator}}"
            var repository = (MagicMirrorRepository)ViewModelLocator.ServiceProvider.GetRequiredService(typeof(MagicMirrorRepository));
            var navigation = (NavigationService)ViewModelLocator.ServiceProvider.GetRequiredService(typeof(INavigationService));
            BindingContext = new MagicMirrorsPageViewModel(repository, navigation);
        }
    }
}