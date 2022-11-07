using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Api;
using DamatMobile.Core.Dtos;
using DynamicData;
using ReactiveUI;

namespace DamatMobile.Core.ViewModels
{

    public class NotificationViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IApiEndpoints _apiEndpoints;
        private readonly IAppSettings _appSettings;
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        public ObservableCollection<NotificationDto> NotificationModels { get; set; } = new();

        public ReactiveCommand<Unit, Unit> LoadNotificationsCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public NotificationViewModel(IDialogService dialogService,INavigationService navigationService , IApiEndpoints apiEndpoints,
            IAppSettings appSettings) :
            base(dialogService)
        {
            _navigationService = navigationService;
            _apiEndpoints = apiEndpoints;
            _appSettings = appSettings;
            LoadNotificationsCommand = ReactiveCommand.CreateFromTask(LoadNotifications);
            BackCommand = ReactiveCommand.CreateFromTask(navigationService.PopNavigateAsync);
            CatchObservableExceptions(LoadNotificationsCommand);
        }

        private async Task LoadNotifications()
        {
            IsLoading = true;
            NotificationModels.Clear();
            var customerNotifications = await _apiEndpoints.GetCustomerNotifications(_appSettings.UserId);
            NotificationModels.AddRange(customerNotifications);
            IsLoading = false;
        }

        public override async Task Appearing()
        {
            await LoadNotificationsCommand.Execute();
        }
    }
}