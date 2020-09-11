using Xamarin.Forms;

namespace MagicMirror.Common.Navigation
{
    public class NavigationPage : INavigationPage
    {
        public void NextPage(Page page)
        {
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
