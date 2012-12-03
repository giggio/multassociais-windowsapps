/*
 * Originally from http://winrtxamltoolkit.codeplex.com/
 */

using System;

namespace MultasSociais.WinStoreApp.Extensions
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