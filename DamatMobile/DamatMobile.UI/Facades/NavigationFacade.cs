using System;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Ui.Views;
using ReactiveUI;
using Xamarin.Forms;

namespace DamatMobile.Ui.Facades
{
    public class NavigationFacade : INavigationFacade
    {
        public INavigation Navigation => Application.Current.MainPage.Navigation;

        public Task PopAsync()
        {
            return Navigation.PopAsync(true);
        }

        public void SetMainPage<TViewModel>(IViewFor<TViewModel> view) where TViewModel : BaseViewModel
        {
            if (view is HomePage app)
            {
                Application.Current.MainPage = new NavigationPage(app);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage((Page)view);
            }
        }

        public Task PushAsync<TViewModel>(IViewFor<TViewModel> view) where TViewModel : BaseViewModel
        {
            if (view is Page page)
                return Navigation.PushAsync(page, true);
            throw new ArgumentException($"{view} is not Page");
        }
    }
}