using Autofac;
using DamatMobile.Core;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.ViewModels;
using DamatMobile.Ui.Facades;
using DamatMobile.Ui.Views;
using ReactiveUI;

namespace DamatMobile.Ui
{
    public class DependencyInitializerUi : DependencyInitializerCore
    {
        public override void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<NavigationFacade>().As<INavigationFacade>().SingleInstance();
            builder.RegisterType<NetworkConnectivity>().As<INetworkConnectivity>().SingleInstance();
            builder.RegisterType<AppSettings>().As<IAppSettings>().SingleInstance();
            builder.RegisterType<DatabasePathProvider>().As<IDatabasePathProvider>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            
            builder.RegisterType<MainPage>().As<IViewFor<MainViewModel>>();
            builder.RegisterType<HomePage>().As<IViewFor<HomeViewModel>>();
            builder.RegisterType<LoginPage>().As<IViewFor<LoginViewModel>>();
            builder.RegisterType<ConfirmCustomerPage>().As<IViewFor<ConfirmCustomerViewModel>>();
            builder.RegisterType<NotificationPage>().As<IViewFor<NotificationViewModel>>();
            builder.RegisterType<BrandImagesDialog>().As<IViewFor<BrandImagesViewModel>>();
        }
    }
}