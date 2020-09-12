using MagicMirror.Models.WiFi;
using Newtonsoft.Json;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMirror.Models.Bluetooth
{
    /// <summary>
    /// Объект для пересылки команд по блутус 
    /// </summary>
    public class BluetoothModel
    {
        /// <summary>
        /// Соединение устройства с wifi сетью
        /// </summary>
        /// <param name="device">Устройство</param>
        /// <param name="wifiRequest">Запрос</param>
        /// <returns>Результат соединения с wifi сетью</returns>
        public async Task<WiFiCredentialsResponse> SetWifiCredentialsAsync(IDevice device, WiFiCredentialsRequest wifiRequest)
        {
            try
            {
                if (device.State == Plugin.BLE.Abstractions.DeviceState.Connected)
                {
                    WiFiResponse response = await GetCharacteristicAsync(device, Constants.WiFiCredentialsCharacteristicsGuid);
                    if (response.IsSuccess)
                    {
                        string requestJson = JsonConvert.SerializeObject(wifiRequest);
                        byte[] data = Encoding.UTF8.GetBytes(requestJson);
                        bool result = await response.Characteristic.WriteAsync(data);
                        byte[] ipData = await response.Characteristic.ReadAsync();

                        var wiFiCredentialsResponse = ipData.Length > 0 ? new WiFiCredentialsResponse
                        {
                            Ip = Encoding.UTF8.GetString(ipData),
                            IsSuccess = true
                        } :
                        new WiFiCredentialsResponse
                        {
                            IsSuccess = false,
                            ErrorCode = (int)BluetoothErrorCode.NotValidCredentials
                        };
                        return wiFiCredentialsResponse;
                    }
                    else
                    {
                        return new WiFiCredentialsResponse() { IsSuccess = false, ErrorCode = response.ErrorCode };
                    }
                }
                else
                {
                    return new WiFiCredentialsResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.NoConnection };
                }
            }
            catch (Exception ex)
            {
                return new WiFiCredentialsResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.Unknown, ErrorInfo = ex.ToString() };
            }
        }

        /// <summary>
        /// Получение списка wifi сетей
        /// </summary>
        /// <param name="device">Устройство к которому подключились</param>
        /// <returns>Список сетей</returns>
        public async Task<WiFiNetworkResponse> GetNetworksAsync(IDevice device)
        {
            try
            {
                if (device.State == Plugin.BLE.Abstractions.DeviceState.Connected)
                {
                    WiFiResponse response = await GetCharacteristicAsync(device, Constants.WiFiNetworksCharacteristicsGuid);
                    if (response.IsSuccess)
                    {
                        var result = await response.Characteristic.WriteAsync(new byte[] { });
                        var data = await response.Characteristic.ReadAsync();
                        var wifiResponseJson = Encoding.UTF8.GetString(data);
                        WiFiNetworkResponse wifiResponse = JsonConvert.DeserializeObject<WiFiNetworkResponse>(wifiResponseJson);
                        return wifiResponse;
                    }
                    else
                    {
                        return new WiFiNetworkResponse() { IsSuccess = false, ErrorCode = response.ErrorCode };
                    }
                }
                else
                {
                    return new WiFiNetworkResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.NoConnection };
                }
            }
            catch (Exception ex)
            {
                return new WiFiNetworkResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.Unknown, ErrorInfo = ex.ToString() };
            }
        }

        private async Task<WiFiResponse> GetCharacteristicAsync(IDevice device, Guid characteristicGuid)
        {
            IReadOnlyList<IService> services = await device.GetServicesAsync();
            if (services == null)
            {
                return new WiFiResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.NoServices };
            }

            var wService = services.Where(d => d.Id == Constants.WifiServiceGuid).FirstOrDefault();
            if (wService == null)
            {
                return new WiFiResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.NoSpecificService };
            }

            ICharacteristic charecteristic = await wService.GetCharacteristicAsync(characteristicGuid);
            if (charecteristic != null)
            {
                return new WiFiResponse() { Characteristic = charecteristic, IsSuccess = true };
            }
            else
            {
                return new WiFiResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.NoSpecificCharecteristic };
            }
        }
    }
}
