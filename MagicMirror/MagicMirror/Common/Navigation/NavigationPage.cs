using MagicMirror.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace MagicMirror.Common.Navigation
{
    public class NavigationPage : INavigationPage
    {
        public void NextPage(Page page)
        {
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NextPage2(Type typeView)
        {
            //перед тем как раскоментить зарегать все вьюхи
            //var page = (Page)ViewModelLocator.ServiceProvider.GetRequiredService(typeView);
            //Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
