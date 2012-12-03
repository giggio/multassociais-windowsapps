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
            if (value.GetType() != typeof(string) || (targetType != typeof(DateTime) && targetType != typeof(object)))
            {
                throw new ArgumentException("Only converts from DateTime to String and back.");
            }
            if (string.IsNullOrWhiteSpace(language))
            {
                language = GlobalizationPreferences.Languages.First();
            }
            return System.Convert.ToDateTime(value, new CultureInfo(language));
        }
    }
    public class SumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null) 
                throw new ArgumentNullException("parameter");
            if (value == null) 
                throw new ArgumentNullException("value");
            return (double)value + System.Convert.ToDouble(parameter);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
