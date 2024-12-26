#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class LinqExtension 
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        var list = source as IList<T> ?? source.ToList();
        foreach (T item in list)
        {
            action(item);
        }
    }
    
    public static IEnumerable<T> EnsureListAndApply<T>(this IEnumerable<T> source, Action<T> action)
    {
        var list = source as IList<T> ?? source.ToList();
        foreach (var item in list)
        {
            action(item);
        }
        return list;
    }
    
    public static bool IsNullOrWhitespace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
    
    public static IEnumerable<(TFirst, TSecond)> ZipWithPair<TFirst, TSecond>(
        this IEnumerable<TFirst> first,
        IEnumerable<TSecond> second)
    {
        return first.Zip(second, (f, s) => (f, s));
    }

    public static IEnumerable<T> Apply<T>(this IEnumerable<T> source, Action<T> action)
    {
        return source.Select(item =>
        {
            action(item);
            return item;
        });
    }
}