using System;
using System.Globalization;
using System.Linq;
using Windows.System.UserProfile;
using Windows.UI.Xaml.Data;

namespace MultasSociais.WinStoreApp
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null) return value;
            return string.Format((string)parameter, value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.GetType() != typeof(DateTime) || targetType != typeof(string))
            {
                throw new ArgumentException("Only converts from DateTime to String.");
            }
            if (string.IsNullOrWhiteSpace(language))
            {
                language = GlobalizationPreferences.Languages.First();
            }
            if (parameter == null) 
                throw new ArgumentNullException("parameter");
            return ((DateTime) value).ToString((string) parameter, new CultureInfo(language));
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
