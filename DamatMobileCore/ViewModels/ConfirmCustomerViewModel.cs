using System.Reactive;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using ReactiveUI;

namespace DamatMobile.Core.ViewModels
{
    public class ConfirmCustomerViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly INavigationService _navigationService;
        private readonly string _phoneNumber;

        public int? Code { get; set; } = null;
        public ReactiveCommand<Unit, Unit> ConfirmCodeCommand { get; }

        public ConfirmCustomerViewModel(IDialogService dialogService, IAuthorizationService authorizationService,
            INavigationService navigationService, string phoneNumber) : base(dialogService)
        {
            _authorizationService = authorizationService;
            _navigationService = navigationService;
            _phoneNumber = phoneNumber;

            ConfirmCodeCommand = ReactiveCommand.CreateFromTask(ConfirmCustomer);
            CatchObservableExceptions(ConfirmCodeCommand);
        }

        private async Task ConfirmCustomer()
        {
            var confirmUser = await _authorizationService.ConfirmUser(Code!.Value, _phoneNumber);
            if (confirmUser is { })
            {
                await _authorizationService.RegisterUser(confirmUser);
                _navigationService.InitMainPage();
            }
        }
    }
}