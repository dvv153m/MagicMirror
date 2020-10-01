Программа для управления зеркалом MagicMirror(https://magicmirror.builders/)
и для подключения Raspberry pi к WiFi сети через Bluetooth 

0) Бизи индикатор в начале загрузки приложения и бизи индикатор загрузки страницы
1) Убедится что сначала вызывается конструктор потом OnLoadedCommand
2) Сделать заглушки для BluetoothService и BluetoothModel чтоб протестить интерфейс
3) Окошко для отображения ошибок с Expander подробнее
4) Сохранение ip (хранения инфы о нескольких устройствах). Получение mac адреса из raspberry


ДОБАВИТЬ отписку от событий
protected override void OnDisappearing()
    {
        MessagingCenter.Unsubscribe<MasterPageViewModel, string>(this, "Detail");
        base.OnDisappearing();
    }

создать репозиторий для хранения кред для нескольких зекрал

разница между async command и обычным ICommand или DelegateCommand
public DelegateCommand TestCommand => new DelegateCommand(async (obj) =>
        {            
            IsBusy = true;
            DownloadSomeDataAsync().ContinueWith((obj1) =>
            {
                IsBusy = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        });