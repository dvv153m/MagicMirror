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
        }
    }
}