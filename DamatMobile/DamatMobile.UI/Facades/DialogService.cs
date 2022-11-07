using DamatMobile.Core.Abstractions;
using Xamarin.Forms;

namespace DamatMobile.Ui.Facades
{
    public class DialogService : IDialogService
    {
        private Page Page => Application.Current.MainPage;

        public void ShowMessage(string title, string message)
        {
            Page.DisplayAlert(title, message,"Ok");
        }
    }
}