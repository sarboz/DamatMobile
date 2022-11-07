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
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IAuthorizationService _authorizationService;
        private readonly INavigationService _navigationService;
        private bool _isFlipped;
        private bool _isProductLoading;
        private bool _isCardLoading;
        private bool _isPurchaseLoading;

        public bool IsFlipped
        {
            get => _isFlipped;
            set => this.RaiseAndSetIfChanged(ref _isFlipped, value);
        }

        public string Username { get; set; }

        public ObservableCollection<Product> Products { get; set; } = new();
        public ObservableCollection<PurchaseHistory> PurchaseHistories { get; set; } = new();
        public ObservableCollection<VirtualCard> Cards { get; set; } = new();
        public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
        public ReactiveCommand<Unit, Unit> CardLoadCommand { get; }
        public ReactiveCommand<Unit, Unit> ProductsLoadCommand { get; }
        public ReactiveCommand<Unit, Unit> PurchaseLoadCommand { get; }
        public ReactiveCommand<Unit, Unit> FlipCommand { get; set; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; set; }
        public ReactiveCommand<Unit, Unit> NotificationCommand { get; set; }
        public ReactiveCommand<Unit, Unit> RegisterTokenCommand { get; }

        public HomeViewModel(IDialogService dialogService, IProductService productService,
            ICustomerService customerService, IAppSettings settings, IAuthorizationService authorizationService, INavigationService navigationService) : base(dialogService)
        {
            _productService = productService;
            _customerService = customerService;
            _authorizationService = authorizationService;
            _navigationService = navigationService;
            Username = settings.UserName;

            RefreshCommand = ReactiveCommand.CreateFromTask(Refresh);
            CardLoadCommand = ReactiveCommand.CreateFromTask(LoadCard);
            PurchaseLoadCommand = ReactiveCommand.CreateFromTask(LoadPurchase);
            ProductsLoadCommand = ReactiveCommand.CreateFromTask(LoadProducts);
            LogoutCommand = ReactiveCommand.Create(Logout);
            RegisterTokenCommand = ReactiveCommand.CreateFromTask(() => _authorizationService.RegisterToken());
            NotificationCommand =
                ReactiveCommand.CreateFromTask(NavigateToNotificationPage);
            
            FlipCommand = ReactiveCommand.Create(Flip);

            CatchObservableExceptions(NotificationCommand,ProductsLoadCommand, RefreshCommand, FlipCommand, RegisterTokenCommand, PurchaseLoadCommand,
                CardLoadCommand);
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

        public bool IsProductLoading
        {
            get => _isProductLoading;
            set => this.RaiseAndSetIfChanged(ref _isProductLoading, value);
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
            IsProductLoading = true;
            Products.Clear();
            var products = await _productService.GetProducts();
            Products.AddRange(products);
            await _productService.SyncProducts(products);
            IsProductLoading = false;
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
            CardLoadCommand.Execute();
            ProductsLoadCommand.Execute();
            return Task.CompletedTask;
        }

        public override async Task Initialize()
        {
            await RefreshCommand.Execute();
            RegisterTokenCommand.Execute();
        }
    }
}