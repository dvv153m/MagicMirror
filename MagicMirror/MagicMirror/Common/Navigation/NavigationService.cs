using MagicMirror.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace MagicMirror.Common.Navigation
{
    public class NavigationService : INavigationService
    {
        /*public void NextPage(Page page)
        {
            Application.Current.MainPage.Navigation.PushAsync(page);
        }*/

        public void NextPage(Type typeView)
        {            
            var page = (Page)ViewModelLocator.ServiceProvider.GetRequiredService(typeView);            
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(page);                            
        }
    }
}
