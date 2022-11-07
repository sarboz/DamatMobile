using Xamarin.Forms.Xaml;

namespace DamatMobile.Ui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmCustomerPage
    {
        public ConfirmCustomerPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CodeEntry.Focus();
        }
    }
}