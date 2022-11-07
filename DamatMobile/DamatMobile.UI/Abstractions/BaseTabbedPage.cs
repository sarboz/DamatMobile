using DamatMobile.Core.Abstractions;
using ReactiveUI;
using Xamarin.Forms;

namespace DamatMobile.Ui.Abstractions
{
    public class BaseTabbedPage<TViewModel> : TabbedPage, IViewFor<TViewModel> where TViewModel : BaseViewModel
    {
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => BindingContext = ViewModel = value as TViewModel;
        }

        public TViewModel ViewModel { get; set; }
    
        private bool isInited;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Appearing();
            if (!isInited)
                ViewModel?.Initialize();
            isInited = true;
        }
    }
}