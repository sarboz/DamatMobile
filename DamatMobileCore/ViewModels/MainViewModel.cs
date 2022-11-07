using System.Reactive;
using DamatMobile.Core.Abstractions;
using ReactiveUI;

namespace DamatMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthorizationService _authorizationService;

        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }
        public ReactiveCommand<Unit, Unit> RegisterTokenCommand { get; }

        public MainViewModel(IDialogService dialogService, INavigationService navigationService,
            IAuthorizationService authorizationService, IAppSettings appSettings) : base(dialogService)
        {
            _navigationService = navigationService;
            _authorizationService = authorizationService;
            UserName = appSettings.UserName;
            LogoutCommand = ReactiveCommand.Create(Logout);
            RegisterTokenCommand = ReactiveCommand.CreateFromTask(() => _authorizationService.RegisterToken());
            CatchObservableExceptions(LogoutCommand);
        }

        private void Logout()
        {
            _authorizationService.Logout();
            _navigationService.InitMainPage();
        }

        public string UserName { get; set; }
    }
}