using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using DamatMobile.Core.Abstractions.Services;
using DamatMobile.Core.Models;
using DynamicData;
using ReactiveUI;

namespace DamatMobile.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IBrandService _brandService;
        private readonly ICustomerService _customerService;
        private readonly INewsService _newsService;
        private readonly IAuthorizationService _authorizationService;
        private readonly INavigationService _navigationService;
        private bool _isFlipped;
        private bool _isBrandsLoading;
        private bool _isCardLoading;
        private bool _isPurchaseLoading;
        private bool _isNewsLoading;

        public bool IsFlipped
        {
            get => _isFlipped;
            set => this.RaiseAndSetIfChanged(ref _isFlipped, value);
        }

        public string Username { get; set; }

        public ObservableCollection<Brand> Brands { get; set; } = new();
        public ObservableCollection<PurchaseHistory> PurchaseHistories { get; set; } = new();
        public ObservableCollection<News> NewsList { get; set; } = new();
        public ObservableCollection<VirtualCard> Cards { get; set; } = new();
        public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
        public ReactiveCommand<Unit, Unit> CardLoadCommand { get; }
        public ReactiveCommand<Unit, Unit> BrandsLoadCommand { get; }
        public ReactiveCommand<Unit, Unit> NewsLoadCommand { get; }
        public ReactiveCommand<Unit, Unit> PurchaseLoadCommand { get; }
        public ReactiveCommand<Unit, Unit> FlipCommand { get; set; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; set; }
        public ReactiveCommand<Unit, Unit> NotificationCommand { get; set; }
        public ReactiveCommand<Unit, Unit> RegisterTokenCommand { get; }

        public HomeViewModel(IDialogService dialogService, IBrandService brandService,
            ICustomerService customerService, 
            INewsService newsService,
            IAppSettings settings,
            IAuthorizationService authorizationService, 
            INavigationService navigationService) : base(dialogService)
        {
            _brandService = brandService;
            _customerService = customerService;
            _newsService = newsService;
            _authorizationService = authorizationService;
            _navigationService = navigationService;
            Username = settings.UserName;

            RefreshCommand = ReactiveCommand.CreateFromTask(Refresh);
            NewsLoadCommand = ReactiveCommand.CreateFromTask(LoadNews);
            CardLoadCommand = ReactiveCommand.CreateFromTask(LoadCard);
            PurchaseLoadCommand = ReactiveCommand.CreateFromTask(LoadPurchase);
            BrandsLoadCommand = ReactiveCommand.CreateFromTask(LoadProducts);
            LogoutCommand = ReactiveCommand.Create(Logout);
            RegisterTokenCommand = ReactiveCommand.CreateFromTask(() => _authorizationService.RegisterToken());
            NotificationCommand =
                ReactiveCommand.CreateFromTask(NavigateToNotificationPage);
            
            FlipCommand = ReactiveCommand.Create(Flip);

            CatchObservableExceptions(NewsLoadCommand,NotificationCommand,BrandsLoadCommand, RefreshCommand, FlipCommand, RegisterTokenCommand, PurchaseLoadCommand,
                CardLoadCommand);
        }

        private async Task LoadNews()
        {
            IsNewsLoading = true;
            NewsList.Clear();
            var  newsList = await _newsService.GetNews();
            NewsList.AddRange(newsList);
            await _newsService.SyncNews(newsList);
            IsNewsLoading = false;
        }

        private Task NavigateToNotificationPage()
        {
          return  _navigationService.PushNavigationAsync<NotificationViewModel>();
        }

        private void Logout()
        {
            _authorizationService.Logout();
            _navigationService.InitMainPage();
        }

        public bool IsBrandsLoading
        {
            get => _isBrandsLoading;
            set => this.RaiseAndSetIfChanged(ref _isBrandsLoading, value);
        }

        public bool IsNewsLoading
        {
            get => _isNewsLoading;
            set => this.RaiseAndSetIfChanged(ref _isNewsLoading, value);
        }
        public bool IsCardLoading
        {
            get => _isCardLoading;
            set => this.RaiseAndSetIfChanged(ref _isCardLoading, value);
        }

        public bool IsPurchaseLoading
        {
            get => _isPurchaseLoading;
            set => this.RaiseAndSetIfChanged(ref _isPurchaseLoading, value);
        }

        private async Task LoadPurchase()
        {
            IsPurchaseLoading = true;
            PurchaseHistories.Clear();
            var customerPurchases = await _customerService.GetCustomerPurchases();
            PurchaseHistories.AddRange(customerPurchases);
            await _customerService.SyncPurchase(customerPurchases);
            IsPurchaseLoading = false;
        }

        private async Task LoadProducts()
        {
            IsBrandsLoading = true;
            Brands.Clear();
            var products = await _brandService.GetBrands();
            Brands.AddRange(products);
            await _brandService.SyncBrands(products);
            IsBrandsLoading = false;
        }

        private async Task LoadCard()
        {
            IsCardLoading = true;
            Cards.Clear();
            var customersCard = await _customerService.GetCustomersCard();
            Cards.AddRange(customersCard);
            await _customerService.SyncCustomerCard(customersCard);
            IsCardLoading = false;
        }

        private void Flip()
        {
            IsFlipped = !IsFlipped;
        }

        private Task Refresh()
        {
            PurchaseLoadCommand.Execute();
            NewsLoadCommand.Execute();
            CardLoadCommand.Execute();
            BrandsLoadCommand.Execute();
            return Task.CompletedTask;
        }

        public override async Task Initialize()
        {
            await RefreshCommand.Execute();
            RegisterTokenCommand.Execute();
        }
    }
}