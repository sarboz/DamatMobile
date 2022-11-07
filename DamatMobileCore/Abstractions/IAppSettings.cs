using System;

namespace DamatMobile.Core.Abstractions
{
    public interface IAppSettings
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public bool IsTokenRegistered { get; set; }
    }
}