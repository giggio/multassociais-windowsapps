using System;
using System.Globalization;
using System.Linq;
#if WINDOWS_PHONE
using System.Windows.Data;
#elif NETFX_CORE
using Windows.UI.Xaml.Data;
#endif
#if WINDOWS_PHONE
namespace MultasSociais.WinPhone8App
#elif NETFX_CORE
namespace MultasSociais.WinStoreApp
#endif
{
    public abstract class BaseValueConverter : IValueConverter
    {
#if WINDOWS_PHONE        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture.ToString());
        }

        public abstract object Convert(object value, Type targetType, object parameter, string language);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack(value, targetType, parameter, culture.ToString());
        }

        public abstract object ConvertBack(object value, Type targetType, object parameter, string language);
#elif NETFX_CORE
        public abstract object Convert(object value, Type targetType, object parameter, string language);
        public abstract object ConvertBack(object value, Type targetType, object parameter, string language);
#endif


    }


    public class StringFormatConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null) return value;
            return string.Format((string)parameter, value);
        }
        public override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
    public class DateFormatConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
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
        public override object ConvertBack(object value, Type targetType, object parameter, string language)
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
#if WINDOWS_PHONE
            return CultureInfo.CurrentCulture.ToString();
#elif NETFX_CORE
            return Windows.Globalization.ApplicationLanguages.Languages.First();
#endif
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
    public class SumConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null) 
                throw new ArgumentNullException("parameter");
            if (value == null) 
                throw new ArgumentNullException("value");
            return (double)value + System.Convert.ToDouble(parameter);
        }
        public override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
