// <auto-generated />
#pragma warning disable

namespace Polyfills;
using System;
using System.Collections.Generic;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if !NET9_0_OR_GREATER

    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview#linq
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1)))")]
    public static IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        TAccumulate seed,
        Func<TAccumulate, TSource, TAccumulate> func,
        IEqualityComparer<TKey>? keyComparer = null)
        where TKey : notnull
    {
        if (source is TSource[] {Length: 0})
        {
            return [];
        }

        return AggregateByIterator(source, keySelector, seed, func, keyComparer);
    }

    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview#linq
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1)))")]
    public static IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TKey, TAccumulate> seedSelector,
        Func<TAccumulate, TSource, TAccumulate> func,
        IEqualityComparer<TKey>? keyComparer = null)
        where TKey : notnull
    {
        if (source is TSource[] {Length: 0})
        {
            return [];
        }

        return AggregateByIterator(source, keySelector, seedSelector, func, keyComparer);
    }

    static IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateByIterator<TSource, TKey, TAccumulate>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey>? keyComparer)
        where TKey : notnull
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        foreach (var item in PopulateDictionary(enumerator, keySelector, seed, func, keyComparer))
        {
            yield return item;
        }

        static Dictionary<TKey, TAccumulate> PopulateDictionary(IEnumerator<TSource> enumerator, Func<TSource, TKey> keySelector, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey>? keyComparer)
        {
            Dictionary<TKey, TAccumulate> dict = new(keyComparer);

            do
            {
                var value = enumerator.Current;
                var key = keySelector(value);

                if (!dict.TryGetValue(key, out var accumulate))
                {
                    accumulate = seed;
                }

                dict[key] = func(accumulate, value);
            } while (enumerator.MoveNext());

            return dict;
        }
    }

    static IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateByIterator<TSource, TKey, TAccumulate>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, TAccumulate> seedSelector, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey>? keyComparer)
        where TKey : notnull
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield break;
        }

        foreach (var item in PopulateDictionary(enumerator, keySelector, seedSelector, func, keyComparer))
        {
            yield return item;
        }

        static Dictionary<TKey, TAccumulate> PopulateDictionary(IEnumerator<TSource> enumerator, Func<TSource, TKey> keySelector, Func<TKey, TAccumulate> seedSelector, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey>? keyComparer)
        {
            var dict = new Dictionary<TKey, TAccumulate>(keyComparer);

            do
            {
                var value = enumerator.Current;
                var key = keySelector(value);

                if (!dict.TryGetValue(key, out var accumulate))
                {
                    accumulate = seedSelector(key);
                }

                dict[key] = func(accumulate, value);
            } while (enumerator.MoveNext());

            return dict;
        }
    }

#endif
}