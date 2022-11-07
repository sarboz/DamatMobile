using System.Reactive;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using ReactiveUI;

namespace DamatMobile.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly INavigationService _navigationService;
        public Validatable<string> Phone { get; set; }
        public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

        public LoginViewModel(IDialogService dialogService, IAuthorizationService authorizationService,
            INavigationService navigationService) : base(dialogService)
        {
            _authorizationService = authorizationService;
            _navigationService = navigationService;
            Phone = Validator.Build<string>()
                .When(x => !string.IsNullOrEmpty(x))
                .Must(x => x.Length == 12, "Phone length is 9");
            SubmitCommand = ReactiveCommand.CreateFromTask(SubmitPhoneNumber);

            CatchObservableExceptions(SubmitCommand);
        }

        private async Task SubmitPhoneNumber()
        {
            if (Phone.Validate())
            {
                var clearedPhoneNumber = Phone.Value.Replace("-", "");
                await _authorizationService.SendVerificationCode(clearedPhoneNumber);
                await _navigationService.PushNavigationAsync<ConfirmCustomerViewModel>(("phoneNumber",
                    clearedPhoneNumber));
            }
        }
    }
}