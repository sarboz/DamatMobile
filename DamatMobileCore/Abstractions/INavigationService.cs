using System.Threading.Tasks;

namespace DamatMobile.Core.Abstractions
{
    public interface INavigationService
    {
        void InitMainPage();
        Task PopNavigateAsync();
        Task PopPopupAsync();
        Task PushNavigationAsync<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;

        Task PopupPushAsync<TViewModel>(params (string paramaterName, object value)[] parameters)
            where TViewModel : BaseViewModel;
        Task PushNavigationAsync<TViewModel>(params (string parametrName, object value)[] parameters)
            where TViewModel : BaseViewModel;
    }
}