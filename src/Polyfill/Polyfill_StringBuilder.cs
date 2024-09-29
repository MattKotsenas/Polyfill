// <auto-generated />
#pragma warning disable

namespace Polyfills;
using System;
using System.Text;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if FeatureMemory && (!NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER)

    /// <summary>
    /// Returns a value indicating whether the characters in this instance are equal to the characters in a specified
    /// read-only character span.
    /// </summary>
    /// <param name="span">The character span to compare with the current instance.</param>
    /// <remarks>
    /// The Equals method performs an ordinal comparison to determine whether the characters in the current instance
    /// and span are equal.
    /// </remarks>
    /// <returns>true if the characters in this instance and span are the same; otherwise, false.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals#system-text-stringbuilder-equals(system-readonlyspan((system-char)))")]
    public static bool Equals(this StringBuilder target, ReadOnlySpan<char> span)
    {
        if (target.Length != span.Length)
        {
            return false;
        }

        for (var index = 0; index < target.Length; index++)
        {
            var ch1 = target[index];
            var ch2 = span[index];
            if (ch1 != ch2)
            {
                return false;
            }
        }

        return true;
    }

#endif

#if !NET9_0_OR_GREATER && FeatureMemory
    /// <summary>
    /// Replaces all instances of one string with another in part of this builder.
    /// </summary>
    /// <param name="oldValue">The string to replace.</param>
    /// <param name="newValue">The string to replace <paramref name="oldValue"/> with.</param>
    /// <param name="startIndex">The index to start in this builder.</param>
    /// <param name="count">The number of characters to read in this builder.</param>
    /// <remarks>
    /// If <paramref name="newValue"/> is <c>null</c>, instances of <paramref name="oldValue"/> are removed from this builder.
    /// </remarks>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char)))")]
    public static StringBuilder Replace(this StringBuilder target, ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue) =>
        target.Replace(oldValue.ToString(), newValue.ToString());

    /// <summary>
    /// Replaces all instances of one read-only character span with another in part of this builder.
    /// </summary>
    /// <param name="oldValue">The read-only character span to replace.</param>
    /// <param name="newValue">The read-only character span to replace <paramref name="oldValue"/> with.</param>
    /// <param name="startIndex">The index to start in this builder.</param>
    /// <param name="count">The number of characters to read in this builder.</param>
    /// <remarks>
    /// If <paramref name="newValue"/> is empty, instances of <paramref name="oldValue"/> are removed from this builder.
    /// </remarks>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32)")]
    public static StringBuilder Replace(this StringBuilder target, ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue, int startIndex, int count) =>
        target.Replace(oldValue.ToString(), newValue.ToString(), startIndex, count);
#endif
}