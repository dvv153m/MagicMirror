using MagicMirror.Common.MVVM;
using MagicMirror.IoC;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace MagicMirror.Common.Navigation
{
    public class NavigationService : INavigationService
    {
        /*public void NextPage(Page page)
        {
            Application.Current.MainPage.Navigation.PushAsync(page);
        }*/

        public void NextPage<T>(object navigationData = null) where T : ContentPage
        {            
            var page = (Page)ViewModelLocator.ServiceProvider.GetRequiredService(typeof(T));
            var bindingContext = page.BindingContext as ViewModelBase;
            if (navigationData != null)
            {
                bindingContext.InitializeAsync(navigationData);
             }
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(page);                            
        }
    }
}
