using Autofac;
using DamatMobile.Core;
using DamatMobile.Core.ViewModels;

namespace DamatMobile.Ui.Helpers
{
    public class ViewModelLocator
    {
        public HomeViewModel HomeViewModel => DependencyInitializerCore.Container.Resolve<HomeViewModel>();
        public MainViewModel MainViewModel => DependencyInitializerCore.Container.Resolve<MainViewModel>();
    }
}