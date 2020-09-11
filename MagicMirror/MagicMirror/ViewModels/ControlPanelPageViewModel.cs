using MagicMirror.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicMirror.ViewModels
{
    public class ControlPanelPageViewModel : ViewModelBase
    {
        public ControlPanelPageViewModel()
        { 
        
        }

        public AsyncCommand TestCommand => new AsyncCommand(async () => 
        {
            int test = 5 + 5;
        });
    }
}
