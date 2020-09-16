using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MagicMirror.Services;
using MagicMirror.Views;
using MagicMirror.IoC;

namespace MagicMirror
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();            
            ViewModelLocator.Init();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
