using MagicMirror.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicMirror.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        { 
        
        }

        public AsyncCommand OnLoadedCommand => new AsyncCommand(async () =>
        {
            int r = 5 + 5;
        });
    }
}
