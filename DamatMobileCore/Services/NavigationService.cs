using System;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.ViewModels;
using ReactiveUI;

namespace DamatMobile.Core.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigationFacade _navigationFacade;
        private readonly IAppSettings _appSettings;
        private readonly IDependencyResolver _dependencyResolver;

        public NavigationService(INavigationFacade navigationFacade,IAppSettings appSettings, IDependencyResolver dependencyResolver)
        {
            _navigationFacade = navigationFacade;
            _appSettings = appSettings;
            _dependencyResolver = dependencyResolver;
        }

        public void InitMainPage()
        {
            if (_appSettings.UserId.Equals(Guid.Empty))
            {
                var viewFor = GetView<LoginViewModel>();
                _navigationFacade.SetMainPage(viewFor);
                return;
            }
            var view = GetView<HomeViewModel>();
            _navigationFacade.SetMainPage(view);
        }

        public Task PopNavigateAsync()
        {
            return _navigationFacade.PopAsync();
        }

        public Task PushNavigationAsync<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            var viewFor = GetView<TViewModel>();
            return _navigationFacade.PushAsync(viewFor);
        }

        public Task PushNavigationAsync<TViewModel>(params (string parametrName, object value)[] parameters)
            where TViewModel : BaseViewModel
        {
            var viewFor = GetView<TViewModel>(parameters);
            return _navigationFacade.PushAsync(viewFor);
        }

        private IViewFor<TViewModel> GetView<TViewModel>(params (string parameter, object value)[] parameters)
            where TViewModel : BaseViewModel
        {
            var baseViewModel = _dependencyResolver.Resolve<TViewModel>(parameters);
            return GetView(baseViewModel);
        }

        private IViewFor<TViewModel> GetView<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            var view = _dependencyResolver.Resolve<IViewFor<TViewModel>>();
            ((IViewFor)view).ViewModel = viewModel;
            return view;
        }
    }
}