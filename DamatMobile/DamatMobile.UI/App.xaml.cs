using System;
using Autofac;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;

namespace DamatMobile.Ui
{
    public partial class App : Application
    {
        private static readonly IContainer Container;

        static App()
        {
            Container = new DependencyInitializerUi().Build();
        }

        public App()
        {
            InitializeComponent();
            AppCenter.Start("ios=d3de4489-7f0b-4143-9e76-431e9f42762a;" +
                            "android=4ad0642f-1f10-49ef-b378-2601ba2ebd4a;",
                typeof(Analytics), typeof(Crashes));

            var navigationService = Container.Resolve<INavigationService>();
            var settings = Container.Resolve<IAppSettings>();

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                settings.Token = p.Token;
                Console.WriteLine(p.Token);
            };
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) => { };

            CrossFirebasePushNotification.Current.OnNotificationOpened += async (source, args) =>
            {
                await navigationService.PushNavigationAsync<NotificationViewModel>();
            };

            navigationService.InitMainPage();
        }
    }
}