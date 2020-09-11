using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MagicMirror.Common
{
    public static class Utils
    {
        public static bool IsBluetoothEnable()
        {
            IBluetoothLE bluetoothLE = CrossBluetoothLE.Current;
            return bluetoothLE.IsOn;
        }

        public static async Task<bool> CheckPermissions(Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {

                    var title = $"{permission} Permission";
                    var question = $"To use this plugin the {permission} permission is required. Please go into Settings and turn on {permission} for the app.";
                    var positive = "Settings";
                    var negative = "Maybe Later";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return permissionStatus == PermissionStatus.Granted;
                }

                request = true;

            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { permission });

                if (!results.ContainsKey(permission))
                {
                    var title = $"{permission} Permission";
                    var question = $"To use the plugin the {permission} permission is required.";
                    var positive = "Settings";
                    var negative = "Maybe Later";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return permissionStatus == PermissionStatus.Granted;
                }
                else
                {
                    return results[permission] == PermissionStatus.Granted;
                }
            }

            return permissionStatus == PermissionStatus.Granted;
        }
    }
}
