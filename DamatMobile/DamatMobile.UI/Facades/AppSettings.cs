using System;
using System.Runtime.CompilerServices;
using DamatMobile.Core.Abstractions;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace DamatMobile.Ui.Facades
{
    public class AppSettings : IAppSettings
    {
        public Guid UserId
        {
            get => Guid.Parse(Preferences.Get(nameof(UserId), Guid.Empty.ToString()));
            set => Preferences.Set(nameof(UserId), value.ToString());
        }

        public string UserName
        {
            get => Preferences.Get(nameof(UserName), string.Empty);
            set => Preferences.Set(nameof(UserName), value);
        }

        public string Token
        {
            get => Preferences.Get(nameof(Token), string.Empty);
            set => Preferences.Set(nameof(Token), value);
        }

        public bool IsTokenRegistered
        {
            get => Preferences.Get(nameof(IsTokenRegistered), false);
            set => Preferences.Set(nameof(IsTokenRegistered), value);
        }

        private T GetValue<T>([CallerMemberName] string key = "", string defaultValue = "")
        {
            var json = Preferences.Get(key, defaultValue);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private void SetValue(object source, [CallerMemberName] string key = "")
        {
            var json = JsonConvert.SerializeObject(source);
            Preferences.Set(key, json);
        }
    }
}