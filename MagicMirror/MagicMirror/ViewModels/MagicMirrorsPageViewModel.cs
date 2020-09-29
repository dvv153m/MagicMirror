using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Repository;
using MagicMirror.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MagicMirror.ViewModels
{
    public class MagicMirrorsPageViewModel : ViewModelBase
    {
        private MagicMirrorRepository _magicMirrorRepository;
        private INavigationService _navigation { get; set; }
        
        public MagicMirrorsPageViewModel(MagicMirrorRepository magicMirrorRepository, INavigationService navigation)        
        {
            _magicMirrorRepository = magicMirrorRepository;
            _navigation = navigation;
            Mirrors = new ObservableCollection<Models.MagicMirror>();            
        }

        public AsyncCommand OnLoadedPreferencesCommand => new AsyncCommand(async () =>
        {
            if (_magicMirrorRepository != null)
            {
                var mirrors = _magicMirrorRepository.GetAll();
                Mirrors = new ObservableCollection<Models.MagicMirror>(mirrors);
                Mirrors.Add(new Models.MagicMirror { Name = "m1", Ip = "2112321312", BleAddress="123" });//todo удалить                
            }
        });

        public AsyncCommand DeleteCommand => new AsyncCommand(async () =>
        {
            if (SelectedMirror != null)
            {
                var item = Mirrors.Where(m => m.Name == SelectedMirror.Name && m.Ip == SelectedMirror.Ip && m.BleAddress == SelectedMirror.BleAddress).FirstOrDefault();
                if (item != null)
                {
                    Mirrors.Remove(item);
                }
            }
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

        private Models.MagicMirror _selectedMirror;
        public Models.MagicMirror SelectedMirror
        {
            get { return _selectedMirror; }
            set
            {
                _selectedMirror = value;
                OnPropertyChanged();
                if (_selectedMirror != null)
                {
                    _navigation?.NextPage(typeof(ControlPanelPage));
                }
                //NextCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
