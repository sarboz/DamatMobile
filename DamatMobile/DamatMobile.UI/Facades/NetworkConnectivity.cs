using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DamatMobile.Core.Abstractions;
using Xamarin.Essentials;

namespace DamatMobile.Ui.Facades
{
    public class NetworkConnectivity : INetworkConnectivity
    {
        private readonly BehaviorSubject<bool> _onConnectivityChanged;

        public NetworkConnectivity()
        {
            IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            _onConnectivityChanged = new BehaviorSubject<bool>(IsConnected);

            Observable.FromEventPattern<EventHandler<ConnectivityChangedEventArgs>, ConnectivityChangedEventArgs>(
                    handler => Connectivity.ConnectivityChanged += handler,
                    handler => Connectivity.ConnectivityChanged -= handler)
                .Select(pattern => pattern.EventArgs.NetworkAccess == NetworkAccess.Internet)
                .Do(isConnected => IsConnected = isConnected)
                .Subscribe(_onConnectivityChanged);

            Connectivity.ConnectivityChanged += ConnectivityOnConnectivityChanged;
        }

        public bool IsConnected { get; set; }
        public IObservable<bool> OnConnectivityChanged => _onConnectivityChanged;

        private void ConnectivityOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
        }
    }
}