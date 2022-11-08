using System;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Ui.Views;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
        public Task PopupPopAsync()
        {
            try
            {
                return PopupNavigation.Instance.PopAsync();
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }

            return Task.CompletedTask;
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
        
        public Task PopupPushAsync<TViewModel>(IViewFor<TViewModel> view, bool animate = true)
            where TViewModel : BaseViewModel
        {
            return PopupNavigation.Instance.PushAsync((PopupPage) view);
        }
    }
}