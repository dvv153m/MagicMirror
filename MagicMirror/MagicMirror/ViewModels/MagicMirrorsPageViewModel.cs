using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Repository;
using MagicMirror.Views;
using System.Collections.ObjectModel;

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

        public AsyncCommand OnLoadedPreferences => new AsyncCommand(async () =>
        {
            if (_magicMirrorRepository != null)
            {
                var mirrors = _magicMirrorRepository.GetAll();
                Mirrors = new ObservableCollection<Models.MagicMirror>(mirrors);
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
                //_navigation?.NextPage(typeof(ControlPanelPage));
                //NextCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
