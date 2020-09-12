using System;
using System.Collections.Generic;
using System.Text;

namespace MagicMirror.Models.Bluetooth
{
    public enum BluetoothErrorCode
    {
        /// <summary>
        /// Неизвестная ошибка
        /// </summary>
        Unknown = 0,

        //todo rename
        Reverse1 = 1,

        /// <summary>
        /// Это ошибка появляется, если ввести неверные ssiD password, перезагрузить raspberry и попробовать получить список wifi сетей
        /// </summary>
        NoWirelessInterface = 2,

        /// <summary>
        /// Не найден сервис
        /// </summary>
        NoServices = 3,

        /// <summary>
        /// Не найден определенный сервис
        /// </summary>
        NoSpecificService = 4,

        /// <summary>
        /// Не найдена определенная характеристика
        /// </summary>
        NoSpecificCharecteristic = 5,

        /// <summary>
        /// Нет соединения с устройством
        /// </summary>
        NoConnection = 6,

        /// <summary>
        /// Не верный ssid или password
        /// </summary>
        NotValidCredentials = 7
    }
}
