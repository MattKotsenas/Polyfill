
#pragma warning disable

// ReSharper disable RedundantUsingDirective
// ReSharper disable PartialTypeWithSinglePart

using System;
using Link = System.ComponentModel.DescriptionAttribute;
using System.Text;
// ReSharper disable RedundantAttributeSuffix

static partial class PolyfillExtensions
{
#if (MEMORYREFERENCED && (NETFRAMEWORK || NETSTANDARD || NETCOREAPP)) || NET5_0

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    /// <param name="destination">The span into which to copy this string's contents</param>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto")]
    public static void CopyTo(this string target, Span<char> destination) =>
        target.AsSpan().CopyTo(destination);

    /// <summary>
    /// Copies the contents of this string into the destination span.
    /// </summary>
    /// <param name="destination">The span into which to copy this string's contents</param>
    /// <returns>true if the data was copied; false if the destination was too short to fit the contents of the string.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto")]
    public static bool TryCopyTo(this string target, Span<char> destination) =>
        target.AsSpan().TryCopyTo(destination);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0

    /// <summary>
    /// Returns the hash code for this string using the specified rules.
    /// </summary>
    /// <param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison.</param>
    /// <returns>A 32-bit signed integer hash code.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode#system-string-gethashcode(system-stringcomparison)")]
    public static int GetHashCode(this string target, StringComparison comparisonType) =>
        FromComparison(comparisonType).GetHashCode(target);

    static StringComparer FromComparison(StringComparison comparison) =>
        comparison switch
        {
            StringComparison.CurrentCulture => StringComparer.CurrentCulture,
            StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
            StringComparison.InvariantCulture => StringComparer.InvariantCulture,
            StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
            StringComparison.Ordinal => StringComparer.Ordinal,
            StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
        };

    /// <summary>
    /// Returns a value indicating whether a specified string occurs within this string, using the specified comparison rules.
    /// </summary>
    /// <param name="value">The string to seek.</param>
    /// <param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison.</param>
    /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-string-system-stringcomparison)")]
    public static bool Contains(this string target, string value, StringComparison comparisonType) =>
        target.IndexOf(value, comparisonType) >= 0;

    /// <summary>
    /// Determines whether this string instance starts with the specified character.
    /// </summary>
    /// <param name="value">The character to compare.</param>
    /// <remarks>This method performs an ordinal (case-sensitive and culture-insensitive) comparison.</remarks>
    /// <returns>true if value matches the beginning of this string; otherwise, false.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")]
    public static bool StartsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        return target[0] == value;
    }

    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    /// <param name="value">The character to seek.</param>
    /// <remarks>This method performs an ordinal (case-sensitive and culture-insensitive) comparison.</remarks>
    /// <returns>true if the value parameter occurs within this string; otherwise, false.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")]
    public static bool EndsWith(this string target, char value)
    {
        if (target.Length == 0)
        {
            return false;
        }

        var lastPos = target.Length - 1;
        return lastPos < target.Length &&
               target[lastPos] == value;
    }

    /// <summary>
    /// Splits a string into a maximum number of substrings based on a specified delimiting character and, optionally,
    /// options. Splits a string into a maximum number of substrings based on the provided character separator,
    /// optionally omitting empty substrings from the result.
    /// </summary>
    /// <param name="separator">A character that delimits the substrings in this instance.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings
    /// and include empty substrings.</param>
    /// <returns>An array that contains at most count substrings from this instance that are delimited by separator.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-stringsplitoptions)")]
    public static string[] Split(this string target, char separator, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split(new[] {separator}, options);

    /// <summary>
    /// Splits a string into a maximum number of substrings based on a specified delimiting character and, optionally,
    /// options. Splits a string into a maximum number of substrings based on the provided character separator,
    /// optionally omitting empty substrings from the result.
    /// </summary>
    /// <param name="separator">A character that delimits the substrings in this instance.</param>
    /// <param name="count">The maximum number of elements expected in the array.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings
    /// and include empty substrings.</param>
    /// <returns>An array that contains at most count substrings from this instance that are delimited by separator.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-int32-system-stringsplitoptions)")]
    public static string[] Split(this string target, char separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
        target.Split(new[] {separator}, count, options);
#endif

#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0
    /// <summary>
    /// Returns a value indicating whether a specified character occurs within this string.
    /// </summary>
    /// <remarks>This method performs an ordinal (case-sensitive and culture-insensitive) comparison.</remarks>
    /// <param name="value">The character to seek.</param>
    /// <returns>true if the value parameter occurs within this string; otherwise, false.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")]
    public static bool Contains(this string target, char value) =>
        target.IndexOf(value) >= 0;
#endif
}