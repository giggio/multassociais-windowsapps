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
                language = ObterLingua();
            }
            if (parameter == null) 
                throw new ArgumentNullException("parameter");
            return Converter((DateTime) value, (string) parameter, new CultureInfo(language));
        }
        public string Converter(DateTime valor, string parametro, CultureInfo cultureInfo)
        {
            return valor.ToString(parametro, cultureInfo);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value.GetType() != typeof(string) || (targetType != typeof(DateTime) && targetType != typeof(object)))
            {
                throw new ArgumentException("Only converts from DateTime to String and back.");
            }
            return Converter((string)value, language);
        }
        public DateTime Converter(string valor, string language = null)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                language = ObterLingua();
            }
            return Converter(valor, new CultureInfo(language));
        }

        private static string ObterLingua()
        {
            return Windows.Globalization.ApplicationLanguages.Languages.First();
        }

        public DateTime Converter(string valor, CultureInfo cultureInfo)
        {
            var date = DateTime.MinValue;
            try
            {
                date = System.Convert.ToDateTime(valor, cultureInfo);
            }
            catch { }
            return date;
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
