using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Net.Wifi;
using Android.Content;
using Android;
using Acr.UserDialogs;
using Android.Bluetooth;
using Android.Locations;
using Xamarin.Forms;
using Android.Gms.Common.Apis;
using Android.Gms.Location;

namespace MagicMirror.Droid
{
    [Activity(Label = "MagicMirror", Icon = "@mipmap/mirror", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly string[] Permissions =
        {
            Manifest.Permission.Bluetooth,
            Manifest.Permission.BluetoothAdmin,
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessWifiState,
            Manifest.Permission.ChangeWifiState,
        };

        private const int REQUEST_CHECK_SETTINGS = 0x1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CheckPermissions();
            UserDialogs.Init(this);

            TurnOnBluetooth();
            TurnOnGps();
            //TurnOnGpsV2();
            TurnOnWiFi();

            LoadApplication(new App());            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// Включение bluetooth
        /// </summary>
        private void TurnOnBluetooth()
        {
            BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (!bluetoothAdapter.IsEnabled)
            {
                bluetoothAdapter.Enable();
            }
        }

        /// <summary>
        /// Включение gps
        /// </summary>        
        private void TurnOnGps()
        {
            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                Forms.Context.StartActivity(gpsSettingIntent);
            }
        }

        /// <summary>
        /// Включение Wi-Fi
        /// </summary>
        private void TurnOnWiFi()
        {            
            WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
            if(!wifiManager.IsWifiEnabled)
            {
                // Active Wi-Fi connection.                
                wifiManager.SetWifiEnabled(true);                                
            }            
        }

        private void CheckPermissions()
        {
            bool minimumPermissionsGranted = true;

            foreach (string permission in Permissions)
            {
                if (CheckSelfPermission(permission) != Permission.Granted)
                {
                    minimumPermissionsGranted = false;
                }
            }

            // If any of the minimum permissions aren't granted, we request them from the user
            if (!minimumPermissionsGranted)
            {
                RequestPermissions(Permissions, 0);
            }
        }

        /// <summary>
        /// Включение gps
        /// </summary>        
        private void TurnOnGpsV2()
        {
            var googleApiClient = new GoogleApiClient.Builder(this).AddApi(LocationServices.API).Build();
            googleApiClient.Connect();

            var locationRequest = LocationRequest.Create();
            locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
            locationRequest.SetInterval(10000);
            locationRequest.SetFastestInterval(10000 / 2);

            var builder = new LocationSettingsRequest.Builder().AddLocationRequest(locationRequest);
            builder.SetAlwaysShow(true);

            var result = LocationServices.SettingsApi.CheckLocationSettings(googleApiClient, builder.Build());
            result.SetResultCallback((LocationSettingsResult callback) =>
            {
                switch (callback.Status.StatusCode)
                {
                    case LocationSettingsStatusCodes.Success:
                        {
                            //DoStuffWithLocation();
                            break;
                        }
                    case LocationSettingsStatusCodes.ResolutionRequired:
                        {
                            try
                            {
                                // Show the dialog by calling startResolutionForResult(), and check the result
                                // in onActivityResult().
                                callback.Status.StartResolutionForResult(this, REQUEST_CHECK_SETTINGS);
                            }
                            catch (IntentSender.SendIntentException e)
                            {
                            }

                            break;
                        }
                    default:
                        {
                            // If all else fails, take the user to the android location settings
                            StartActivity(new Intent(Android.Provider.Settings.ActionLocationSourceSettings));
                            break;
                        }
                }
            });
        }

        protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case REQUEST_CHECK_SETTINGS:
                    {
                        switch (resultCode)
                        {
                            case Android.App.Result.Ok:
                                {
                                    //DoStuffWithLocation();
                                    break;
                                }
                            case Android.App.Result.Canceled:
                                {
                                    //No location
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
    }
}