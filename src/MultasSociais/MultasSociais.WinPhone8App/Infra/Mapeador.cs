using System;
using System.Windows.Navigation;

namespace MultasSociais.WinPhone8App.Infra
{
    internal class Mapeador : UriMapperBase
    {
        public override Uri MapUri(Uri uri)
        {
            var stringUri = uri.ToString();

            if ((stringUri.Contains("ShareContent")) && (stringUri.Contains("FileId")))
            {
                // Launch from the photo share picker.
                // Incoming URI example: /Views/MainView.xaml?Action=ShareContent&FileId=%7BA3D54E2D-7977-4E2B-B92D-3EB126E5D168%7D
                return RedirectToPhotoShareView(stringUri);
            }

            return uri;
        }

        private static Uri RedirectToPhotoShareView(string stringUri)
        {
            var mappedUri = stringUri.Replace("MainView", "PhotoShareView");
            return new Uri(mappedUri, UriKind.Relative);
        }
    }
}