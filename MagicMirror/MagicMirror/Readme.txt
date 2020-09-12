Программа для управления зеркалом MagicMirror(https://magicmirror.builders/)
и для подключения Raspberry pi к WiFi сети через Bluetooth 

1) Убедится что сначала вызывается конструктор потом OnLoadedCommand
2) Сделать заглушки для BluetoothService и BluetoothModel чтоб протестить интерфейс
3) Раскоментить в Ioc строки:
GetType().Assembly.GetTypes()
                 .Where(type => type.IsClass)
                 .Where(type => type.Name.EndsWith("PageViewModel"))
                 .ToList()
                 .ForEach(viewModel => services.AddTransient(viewModel));
и сделать тоже самое для View
4) Окошко для отображения ошибок с Expander подробнее
5) В FinishPageViewModel в GoToMainViewCommand нужно указать MainPage или ControlPanelPage
6) Сохранение ip (хранения инфы о нескольких устройствах). Получение mac адреса из raspberry