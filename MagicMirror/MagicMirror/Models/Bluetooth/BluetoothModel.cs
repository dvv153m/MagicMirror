﻿using MagicMirror.Models.WiFi;
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
        /// Соединение устройства с wifi сетью (способ 1)
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
                        byte[] data2 = await response.Characteristic.ReadAsync();
                        var wiFiConnectionResponseJson = Encoding.UTF8.GetString(data2);
                        WiFiConnectionResponse wiFiConnectionResponse = JsonConvert.DeserializeObject<WiFiConnectionResponse>(wiFiConnectionResponseJson);
                        
                        var wiFiCredentialsResponse = wiFiConnectionResponse.Ip.Length > 0 ? new WiFiCredentialsResponse
                        {
                            Ip = wiFiConnectionResponse.Ip,
                            BleAddress  = wiFiConnectionResponse.BleAddress,
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
        /// Соединение устройства с wifi сетью (способ 2). Сохранение wpa_supplicant и перезагрузка.
        /// </summary>
        /// <param name="device">Устройство</param>
        /// <param name="wifiRequest">Запрос</param>
        /// <returns>Результат соединения с wifi сетью</returns>
        public async Task SetWifiCredentialsV2Async(IDevice device, WiFiCredentialsRequest wifiRequest)
        {
            try
            {
                if (device.State == Plugin.BLE.Abstractions.DeviceState.Connected)
                {
                    WiFiResponse response = await GetCharacteristicAsync(device, Constants.WiFiCredentialsCharacteristicsV2Guid);
                    if (response.IsSuccess)
                    {
                        string requestJson = JsonConvert.SerializeObject(wifiRequest);
                        byte[] data = Encoding.UTF8.GetBytes(requestJson);
                        //см чтобудет после выполнения метода. На распберике будет перезагрузка
                        bool result = await response.Characteristic.WriteAsync(data);
                        
                        /*byte[] data2 = await response.Characteristic.ReadAsync();
                        var wiFiConnectionResponseJson = Encoding.UTF8.GetString(data2);
                        WiFiConnectionResponse wiFiConnectionResponse = JsonConvert.DeserializeObject<WiFiConnectionResponse>(wiFiConnectionResponseJson);

                        var wiFiCredentialsResponse = wiFiConnectionResponse.Ip.Length > 0 ? new WiFiCredentialsResponse
                        {
                            Ip = wiFiConnectionResponse.Ip,
                            BleAddress = wiFiConnectionResponse.BleAddress,
                            IsSuccess = true
                        } :
                        new WiFiCredentialsResponse
                        {
                            IsSuccess = false,
                            ErrorCode = (int)BluetoothErrorCode.NotValidCredentials
                        };
                        return wiFiCredentialsResponse;*/
                    }
                    /*else
                    {
                        return new WiFiCredentialsResponse() { IsSuccess = false, ErrorCode = response.ErrorCode };
                    }*/
                }
                else
                {
                    return ;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// Получение списка wifi сетей
        /// </summary>
        /// <param name="device">Устройство к которому подключились</param>
        /// <returns>Список сетей</returns>
        public async Task<WiFiNetworksResponse> GetNetworksAsync(IDevice device)
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
                        WiFiNetworksResponse wifiResponse = JsonConvert.DeserializeObject<WiFiNetworksResponse>(wifiResponseJson);
                        return wifiResponse;
                    }
                    else
                    {
                        return new WiFiNetworksResponse() { IsSuccess = false, ErrorCode = response.ErrorCode };
                    }
                }
                else
                {
                    return new WiFiNetworksResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.NoConnection };
                }
            }
            catch (Exception ex)
            {
                return new WiFiNetworksResponse() { IsSuccess = false, ErrorCode = (int)BluetoothErrorCode.Unknown, ErrorInfo = ex.ToString() };
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
