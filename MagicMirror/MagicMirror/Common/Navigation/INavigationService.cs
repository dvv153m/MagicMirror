using System;
using Xamarin.Forms;

namespace MagicMirror.Common.Navigation
{
    public interface INavigationService
    {
        //void NextPage(Page page);
        void NextPage(Type typeView);
    }
}
