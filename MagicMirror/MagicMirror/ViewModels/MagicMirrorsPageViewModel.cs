using MagicMirror.Common.MVVM;
using MagicMirror.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MagicMirror.ViewModels
{
    public class MagicMirrorsPageViewModel : ViewModelBase
    {
        private MagicMirrorRepository _magicMirrorRepository;

        public MagicMirrorsPageViewModel(MagicMirrorRepository magicMirrorRepository)
        {
            _magicMirrorRepository = magicMirrorRepository;
        }

        public AsyncCommand OnLoadedPreferences => new AsyncCommand(async () =>
        {
            var mirrors = _magicMirrorRepository.GetAll();
            Mirrors = new ObservableCollection<Models.MagicMirror>(mirrors);
        });

        private ObservableCollection<Models.MagicMirror> _mirrors { get; set; }
        public ObservableCollection<Models.MagicMirror> Mirrors
        {
            get { return _mirrors; }
            set
            {
                _mirrors = value;
                OnPropertyChanged();
            }
        }
    }
}
