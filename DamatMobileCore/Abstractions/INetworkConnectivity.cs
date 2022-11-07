using System;

namespace DamatMobile.Core.Abstractions
{
    public interface INetworkConnectivity
    {
        bool IsConnected { get; }
        IObservable<bool> OnConnectivityChanged { get; }
    }
}