// <auto-generated />

#pragma warning disable

#if PolyGuard

namespace Polyfills;

using System.Runtime.CompilerServices;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#if PolyPublic
public
#endif

static partial class Guard
{
    public static string NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string argumentName = "")
    {
        if (value is null)
        {
            throw new ArgumentNullException(argumentName);
        }

        if (value.Length == 0)
        {
            throw new ArgumentException("Argument cannot be empty.", argumentName);
        }

        return value;
    }

    public static T NotNullOrEmpty<T>(
        [NotNull] T? value,
        [CallerArgumentExpression("value")] string argumentName = "")
        where T : IEnumerable
    {
        if (value is null)
        {
            throw new ArgumentNullException(argumentName);
        }

        NotEmpty(value);

        return value;
    }


#if FeatureMemory
    public static Memory<char> NotNullOrEmpty(
        [NotNull] Memory<char>? value,
        [CallerArgumentExpression("value")] string argumentName = "")
    {
        if (value == null)
        {
            throw new ArgumentNullException(argumentName);
        }

        if (value.Value.Length == 0)
        {
            throw new ArgumentException("Argument cannot be empty.", argumentName);
        }

        foreach (var ch in value.Value.Span)
        {
            if (!char.IsWhiteSpace(ch))
            {
                return value.Value;
            }
        }

        throw new ArgumentException("Argument cannot be whitespace.", argumentName);
    }

    public static ReadOnlyMemory<char> NotNullOrEmpty(
        [NotNull] ReadOnlyMemory<char>? value,
        [CallerArgumentExpression("value")] string argumentName = "")
    {
        if (value == null)
        {
            throw new ArgumentNullException(argumentName);
        }

        if (value.Value.Length == 0)
        {
            throw new ArgumentException("Argument cannot be empty.", argumentName);
        }

        foreach (var ch in value.Value.Span)
        {
            if (!char.IsWhiteSpace(ch))
            {
                return value.Value;
            }
        }

        throw new ArgumentException("Argument cannot be whitespace.", argumentName);
    }
#endif
}
#endif