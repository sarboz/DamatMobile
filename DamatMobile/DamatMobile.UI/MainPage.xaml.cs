using Autofac;
using DamatMobile.Core;
using DamatMobile.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace DamatMobile.Ui
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        private bool _isInit;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = DependencyInitializerCore.Container.Resolve<MainViewModel>();
        }

     
        
    }
}