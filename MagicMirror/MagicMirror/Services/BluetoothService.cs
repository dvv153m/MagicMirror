using MagicMirror.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MagicMirror.Services
{
    /// <summary>
    /// Сервис для работы с bluetooth low energy
    /// </summary>
    public class BluetoothService
    {
        private IBluetoothLE _ble;

        private IAdapter _adapter;

        public BluetoothService()
        {
            _ble = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;
            _ble.StateChanged += (s, e) =>
            {
                //todo через шину сообщений уведомлять
                //Debug.WriteLine($"BleService: state changed to {e.NewState}");
            };

            _adapter.ScanTimeout = 5000;
            _adapter.ScanMode = ScanMode.Balanced;

            _adapter.DeviceDiscovered += (s, a) =>
            {
                MessagingCenter.Send(this, Constants.AddDeviceTopic, a.Device);
            };

            _adapter.ScanTimeoutElapsed += (s, e) =>
            {
                MessagingCenter.Send(this, Constants.ScanStoppedTopic);
            };
        }

        /// <summary>
        /// Сканирование блутус устройств
        /// </summary>                
        public async Task StartScanAsync(Guid[] serviceUuids = null)
        {
            if (!_adapter.IsScanning)
            {
                await _adapter.StartScanningForDevicesAsync(serviceUuids);
            }
        }

        /// <summary>
        /// Подключение к устройству
        /// </summary>
        /// <param name="device">Блутус устройство</param>
        /// <returns>Результат подключения</returns>
        public async Task<bool> ConnectAsync(IDevice device)
        {
            if (device == null)
            {
                return false;
            }

            if (!_adapter.IsScanning)
            {
                await _adapter.StopScanningForDevicesAsync();
            }

            try
            {
                await _adapter.ConnectToDeviceAsync(device);
                return true;
            }
            catch (DeviceConnectionException ex)
            {
                //Debug.WriteLine($"BleService: connection exception {ex}");
            }
            catch (Exception ex)
            {
                //Debug.WriteLine($"BleService: generic exception {ex}");
            }
            return await Task.FromResult(false);
        }

        public async Task DisconnectAsync(IDevice device)
        {
            await _adapter.DisconnectDeviceAsync(device);
        }

        //todo вынести в отдельный класс который проверяет permissions и состояние блутуса и gps
        public BluetoothState GetBluetoothState()
        {
            return _ble.State;
        }
    }
}
