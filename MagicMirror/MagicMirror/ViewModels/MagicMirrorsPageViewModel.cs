using MagicMirror.Common.MVVM;
using MagicMirror.Common.Navigation;
using MagicMirror.Repository;
using MagicMirror.Views;
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
                Mirrors.Add(new Models.MagicMirror { Name = "11212", Ip = "12121212" });                
            }
        });

        public AsyncCommand<string> DeleteCommand => new AsyncCommand<string>(async (bleAddress) =>
        {
            var item = Mirrors.Where(m => m.BleAddress == bleAddress).FirstOrDefault();
            if (item != null)
            {
                _magicMirrorRepository.Remove(item);
                Mirrors.Remove(item);
            }
        });

        public AsyncCommand<string> EditCommand => new AsyncCommand<string>(async (bleAddress) =>
        {
            TestV = true;
            OnPropertyChanged("TestV");
            //string result = await DisplayPromptAsync("Edit", "Edit MagicMirror name");
            /*var item = Mirrors.Where(m => m.BleAddress == bleAddress).FirstOrDefault();
            if (item != null)
            {
                Mirrors.Remove(item);
            }*/
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
                    _navigation?.NextPage(typeof(ControlPanelPage), SelectedMirror);
                }
                //NextCommand.RaiseCanExecuteChanged();
            }
        }

        public bool TestV { get; set; }
    }
}
