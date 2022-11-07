using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;

namespace DamatMobile.Droid
{
    [Activity(Label = "ImperoArm",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/SplashTheme",
        MainLauncher = true,
        NoHistory = true, LaunchMode = LaunchMode.SingleTop,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity:AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var mainActivityIntent = new Intent(this, typeof(MainActivity));

            if (Intent?.Extras != null)
                mainActivityIntent.PutExtras(Intent.Extras);
            mainActivityIntent.SetFlags(ActivityFlags.SingleTop);
            StartActivity(mainActivityIntent);
        }
    }
}