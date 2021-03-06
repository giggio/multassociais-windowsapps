﻿/*
 * Originally from http://winrtxamltoolkit.codeplex.com/
 */
using System;
#if WINDOWS_PHONE
using System.Windows.Controls;
#elif NETFX_CORE
using Windows.UI.Xaml.Controls;
#endif

#if WINDOWS_PHONE
namespace MultasSociais.WinPhone8App.Extensions
#elif NETFX_CORE
namespace MultasSociais.WinStoreApp.Extensions
#endif
{
    public class TextBoxFormatValidationHandler
    {
        private TextBox textBox;

        internal void Detach()
        {
            textBox.TextChanged -= OnTextBoxTextChanged;
            textBox = null;
        }

        internal void Attach(TextBox textBox)
        {
            if (this.textBox == textBox)
            {
                return;
            }

            if (this.textBox != null)
            {
                Detach();
            }

            this.textBox = textBox;
            this.textBox.TextChanged += OnTextBoxTextChanged;

            Validate();
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }

        internal void Validate()
        {
            var format = TextBoxValidationExtensions.GetFormat(textBox);

            var expectNonEmpty = (format & ValidTextBoxFormats.NonEmpty) != 0;
            var expectStartsWith = (format & ValidTextBoxFormats.StartsWith) != 0;
            var isEmpty = string.IsNullOrWhiteSpace(textBox.Text);

            if (expectNonEmpty && isEmpty)
            {
                MarkInvalid();
                return;
            }

            var startsWith = textBox.Text.StartsWith(TextBoxValidationExtensions.GetStartsWith(textBox), StringComparison.CurrentCultureIgnoreCase);
            if (expectStartsWith && !startsWith && !isEmpty)
            {
                MarkInvalid();
                return;
            }

            var expectNumber = (format & ValidTextBoxFormats.Numeric) != 0;

            if (expectNumber &&
                !isEmpty &&
                !IsNumeric())
            {
                MarkInvalid();
                return;
            }
            
            var expectDateTime = (format & ValidTextBoxFormats.DateTime) != 0;

            if (expectDateTime &&
                !isEmpty &&
                !IsDateTime())
            {
                MarkInvalid();
                return;
            }

            MarkValid();
        }

        private bool IsDateTime()
        {
            var isDateTime = new DateFormatConverter().Converter(textBox.Text) != DateTime.MinValue;
            return isDateTime;
        }

        private bool IsNumeric()
        {
            double number;
            return double.TryParse(textBox.Text, out number);
        }

        protected virtual void MarkValid()
        {
            TextBoxValidationExtensions.SetIsValid(textBox, true);
        }

        protected virtual void MarkInvalid()
        {
            TextBoxValidationExtensions.SetIsValid(textBox, false);
        }
    }
}