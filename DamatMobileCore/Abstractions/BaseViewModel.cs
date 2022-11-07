using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DamatMobile.Core.Dtos;
using DamatMobile.Core.Exceptions;
using ReactiveUI;
using Refit;

namespace DamatMobile.Core.Abstractions
{
    public abstract class BaseViewModel : ReactiveObject
    {
        private readonly IDialogService DialogService;
        public object ViewModel { get; set; }

        public BaseViewModel(IDialogService dialogService)
        {
            DialogService = dialogService;
        }

        protected Task CatchObservableExceptions(params IHandleObservableErrors[] commands)
        {
            var thrownExceptions = MergeExceptionObservable(commands);

            async void OnNext(Exception exception) => await OnErrorShowMessage(exception);

            thrownExceptions.Subscribe(OnNext);
            return Task.CompletedTask;
        }

        protected async Task OnErrorShowMessage(Exception exception)
        {
            switch (exception)
            {
                case DialogMessageException messageException:
                    DialogService.ShowMessage(messageException.Title, messageException.Message);
                    break;
                case ApiException apiException:
                    var exceptionModel = await apiException.GetContentAsAsync<ApiExceptionModel>();
                    DialogService.ShowMessage("Server Error", exceptionModel?.Detail);
                    break;
                default:
                    DialogService.ShowMessage("Упс что-то пошло не так.", exception.GetBaseException().Message);
                    break;
            }

            return;
        }

        private IObservable<Exception>
            MergeExceptionObservable(params IHandleObservableErrors[] handleObservableErrors) =>
            handleObservableErrors.Select(errors => errors.ThrownExceptions).Merge();

        public virtual Task Initialize()
        {
            return Task.CompletedTask;
        }
        
        public virtual Task Appearing()
        {
            return Task.CompletedTask;
        }
    }
}