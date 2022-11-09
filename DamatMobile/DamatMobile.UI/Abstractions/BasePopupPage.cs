using DamatMobile.Core.Abstractions;
using ReactiveUI;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace DamatMobile.Ui.Abstractions
{
    public abstract class BasePopupPage<TViewModel> : PopupPage, IViewFor<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected BasePopupPage()
        {
            Visual = VisualMarker.Material;
        }

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