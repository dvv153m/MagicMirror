﻿
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MagicMirror.Common.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
