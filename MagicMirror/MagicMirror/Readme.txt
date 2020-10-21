Программа для управления зеркалом MagicMirror(https://magicmirror.builders/)
и для подключения Raspberry pi к WiFi сети через Bluetooth 


Сделать бизи индикатор контрол
https://forums.xamarin.com/discussion/63175/how-to-show-activityindicator-in-the-middle-of-the-screen
https://forums.xamarin.com/discussion/168361/xamarin-forms-update-activityindicator-text-from-viewmodel-not-working

0) Xamarin Forms splash screen.
1) Убедится что сначала вызывается конструктор потом OnLoadedCommand
2) Сделать заглушки для BluetoothService и BluetoothModel чтоб протестить интерфейс
3) Окошко для отображения ошибок с Expander подробнее


ДОБАВИТЬ отписку от событий
protected override void OnDisappearing()
    {
        MessagingCenter.Unsubscribe<MasterPageViewModel, string>(this, "Detail");
        base.OnDisappearing();
    }

разница между async command и обычным ICommand или DelegateCommand
public DelegateCommand TestCommand => new DelegateCommand(async (obj) =>
        {            
            IsBusy = true;
            DownloadSomeDataAsync().ContinueWith((obj1) =>
            {
                IsBusy = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        });