using DamatMobile.Core.Abstractions;
using ReactiveUI;
using Xamarin.Forms;

namespace DamatMobile.Ui.Abstractions
{
    public abstract class BaseContentPage<TViewModel> : ContentPage, IViewFor<TViewModel>
        where TViewModel : BaseViewModel
    {
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => BindingContext = ViewModel = value as TViewModel;
        }

        public TViewModel ViewModel { get; set; }

        private bool isInit = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.Appearing();
            if (isInit) return;
            ViewModel?.Initialize();
            isInit = true;
        }
    }
}