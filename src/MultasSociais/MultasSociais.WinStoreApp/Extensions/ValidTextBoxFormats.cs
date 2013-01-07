/*
 * Originally from http://winrtxamltoolkit.codeplex.com/
 */

using System;

#if WINDOWS_PHONE
namespace MultasSociais.WinPhone8App.Extensions
#elif NETFX_CORE
namespace MultasSociais.WinStoreApp.Extensions
#endif
{
    [Flags]
    public enum ValidTextBoxFormats
    {
        Any = 0,
        NonEmpty = 1,
        Numeric = 2,
        NonEmptyNumeric = 3,
        StartsWith = 4,
        StartsWithNonEmpty = 5,
        DateTime = 8,
        DateTimeNonEmpty = 9
    }
}