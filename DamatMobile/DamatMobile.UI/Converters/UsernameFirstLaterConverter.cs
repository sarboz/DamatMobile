using System;
using System.Globalization;
using Xamarin.Forms;

namespace DamatMobile.Ui.Converters
{
    public class UsernameFirstLaterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string userName) return value;
            userName = userName.ToUpper();
            var strings = userName.Split(" ");
            return strings.Length > 1 ? $"{strings[0][0]}{strings[1][0]}" : $"{userName[0]}{userName[1]}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}