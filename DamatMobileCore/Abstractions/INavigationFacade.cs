using System.Threading.Tasks;
using ReactiveUI;

namespace DamatMobile.Core.Abstractions
{
    public interface INavigationFacade
    {
        Task PopAsync();
        void SetMainPage<TViewModel>(IViewFor<TViewModel> view) where TViewModel : BaseViewModel;
        Task PushAsync<TViewModel>(IViewFor<TViewModel> view) where TViewModel : BaseViewModel;
    }
}