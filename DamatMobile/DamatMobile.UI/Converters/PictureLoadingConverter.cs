using System;
using System.Globalization;
using Xamarin.Forms;

namespace DamatMobile.Ui.Converters
{
    public class PictureLoadingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new UriImageSource {Uri = new Uri((string) value)};
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}