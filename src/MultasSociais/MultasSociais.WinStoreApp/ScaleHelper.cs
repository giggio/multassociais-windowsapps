using System;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace MultasSociais.WinStoreApp
{
    public class ScaleHelper
    {
        private static Rect bounds;
        private static ScreenResolution screenResolution;
        private static Rect GetBounds()
        {
            if (bounds == default(Rect))
            {
                bounds = Window.Current.Bounds;
            }
            
            return bounds;
        }
        private static ScreenResolution GetScreenResolution()
        {
            if (screenResolution == ScreenResolution.Unknown)
            {
                var bounds = GetBounds();
                if (bounds.Height < 768)
                    screenResolution = ScreenResolution.Small;
                else if (bounds.Height < 1080)
                    screenResolution = ScreenResolution.Medium;
                else
                    screenResolution = ScreenResolution.Large;
            }
            return screenResolution;
        }
        public Size GroupDetailItemSize
        {
            get
            {
                switch (GetScreenResolution())
                {
                    case ScreenResolution.Small:
                        return new Size(110, 110);
                    case ScreenResolution.Medium:
                        return new Size(140, 140);
                    case ScreenResolution.Large:
                        return new Size(300, 300);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }public Size GroupItemsItemSize
        {
            get
            {
                switch (GetScreenResolution())
                {
                    case ScreenResolution.Small:
                    case ScreenResolution.Medium:
                        return new Size(250, 250);
                    case ScreenResolution.Large:
                        return new Size(400, 400);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public enum ScreenResolution
    {
        Unknown = 0,
        Small,
        Medium,
        Large
    }
}
