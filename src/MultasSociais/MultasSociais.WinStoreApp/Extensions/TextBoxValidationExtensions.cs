/*
 * Originally from http://winrtxamltoolkit.codeplex.com/
 */

using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MultasSociais.WinStoreApp.Extensions
{
    public static class TextBoxValidationExtensions
    {
        #region IsValid
        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.RegisterAttached("IsValid", typeof(bool), typeof(TextBoxValidationExtensions), new PropertyMetadata(true, (d, e) => SetIsValid((TextBox)d, (bool)e.NewValue)));

        private static void SetIsValid(TextBox textBox, bool isValid)
        {
            var brush = isValid ? GetValidBrush(textBox) : GetInvalidBrush(textBox);
            textBox.Background = brush;
        }

        public static bool GetIsValid(DependencyObject d)
        {
            return (bool)d.GetValue(IsValidProperty);
        }
        public static void SetIsValid(DependencyObject d, bool value)
        {
            d.SetValue(IsValidProperty, value);
        }
        #endregion

        #region Format
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.RegisterAttached("Format", typeof(ValidTextBoxFormats), typeof(TextBoxValidationExtensions), new PropertyMetadata(ValidTextBoxFormats.Any, (d, e) => SetupAndValidate(d)));
        public static ValidTextBoxFormats GetFormat(DependencyObject d)
        {
            return (ValidTextBoxFormats)d.GetValue(FormatProperty);
        }
        public static void SetFormat(DependencyObject d, ValidTextBoxFormats value)
        {
            d.SetValue(FormatProperty, value);
        }

        #endregion

        #region StartsWith
        
        public static readonly DependencyProperty StartsWithProperty =
            DependencyProperty.Register("StartsWith", typeof(string), typeof(TextBoxValidationExtensions), new PropertyMetadata(string.Empty, (d, e) => SetupAndValidate(d)));

        public static string GetStartsWith(DependencyObject d)
        {
            return (string)d.GetValue(StartsWithProperty);
        }
        public static void SetStartsWith(DependencyObject d, string startsWith)
        {
            d.SetValue(StartsWithProperty, startsWith);
        }

        #endregion

        #region FormatValidationHandler
        
        public static readonly DependencyProperty FormatValidationHandlerProperty =
            DependencyProperty.RegisterAttached(
                "FormatValidationHandler",
                typeof(TextBoxFormatValidationHandler),
                typeof(TextBoxValidationExtensions),
                new PropertyMetadata(null, OnFormatValidationHandlerChanged));

        public static TextBoxFormatValidationHandler GetFormatValidationHandler(DependencyObject d)
        {
            return (TextBoxFormatValidationHandler)d.GetValue(FormatValidationHandlerProperty);
        }

        public static void SetFormatValidationHandler(DependencyObject d, TextBoxFormatValidationHandler value)
        {
            d.SetValue(FormatValidationHandlerProperty, value);
        }

        private static void OnFormatValidationHandlerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldFormatValidationHandler = (TextBoxFormatValidationHandler)e.OldValue;
            var newFormatValidationHandler = (TextBoxFormatValidationHandler)d.GetValue(FormatValidationHandlerProperty);

            if (oldFormatValidationHandler != null)
            {
                oldFormatValidationHandler.Detach();
            }
            newFormatValidationHandler.Attach((TextBox)d);
        }
        #endregion

        #region ValidBrush
        public static readonly DependencyProperty ValidBrushProperty = 
            DependencyProperty.RegisterAttached("ValidBrush", typeof(Brush), typeof(TextBoxValidationExtensions), new PropertyMetadata(new SolidColorBrush(Colors.White), (d, e) => SetupAndValidate(d)));

        public static Brush GetValidBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(ValidBrushProperty);
        }

        public static void SetValidBrush(DependencyObject d, Brush value)
        {
            d.SetValue(ValidBrushProperty, value);
        }

        #endregion

        #region InvalidBrush
        public static readonly DependencyProperty InvalidBrushProperty = 
            DependencyProperty.RegisterAttached("InvalidBrush", typeof(Brush), typeof(TextBoxValidationExtensions), new PropertyMetadata(new SolidColorBrush(Colors.Pink), (d, e) => SetupAndValidate(d)));

        public static Brush GetInvalidBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(InvalidBrushProperty);
        }
        
        public static void SetInvalidBrush(DependencyObject d, Brush value)
        {
            d.SetValue(InvalidBrushProperty, value);
        }

        #endregion

        private static void SetupAndValidate(DependencyObject dependencyObject)
        {
            SetupAndValidate((TextBox) dependencyObject);
        }
        private static void SetupAndValidate(TextBox textBox)
        {
            TextBoxFormatValidationHandler handler;

            if ((handler = GetFormatValidationHandler(textBox)) == null)
            {
                handler = new TextBoxFormatValidationHandler();
                SetFormatValidationHandler(textBox, handler);
            }
            else
            {
                handler.Validate();
            }
        }
    }
}