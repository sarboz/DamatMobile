using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using DamatMobile.Ui;
using FFImageLoading.Forms.Platform;
using Firebase;
using Microsoft.AppCenter.Crashes;
using Plugin.FirebasePushNotification;
using Platform = Xamarin.Essentials.Platform;

namespace DamatMobile.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = false, LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            FirebaseApp.InitializeApp(this);
            
            Rg.Plugins.Popup.Popup.Init(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                Crashes.TrackError(args.Exception);
                args.Handled = true;
            };

            LoadApplication(new App());
            FirebasePushNotificationManager.ProcessIntent(this,Intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
    }
}