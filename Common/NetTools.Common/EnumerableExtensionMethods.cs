using System;
using System.Collections.Generic;
using System.Linq;

namespace NetTools.Common;

public static class EnumerableExtensionMethods
{
    public static IEnumerable<T> CastToArray<T>(this IEnumerable<object?> elements)
    {
        return elements.Cast<T>().ToArray();
    }

    public static Array CastToGenericArray(this IEnumerable<int> elements)
    {
        return elements.Cast<object?>().ToArray();
    }

    public static Array CastToGenericArray(this IEnumerable<long> elements)
    {
        return elements.Cast<object?>().ToArray();
    }

    public static Array CastToGenericArray(this IEnumerable<float> elements)
    {
        return elements.Cast<object?>().ToArray();
    }

    public static Array CastToGenericArray(this IEnumerable<double> elements)
    {
        return elements.Cast<object?>().ToArray();
    }

    public static Array CastToGenericArray(this IEnumerable<decimal> elements)
    {
        return elements.Cast<object?>().ToArray();
    }

    public static Array CastToGenericArray(this IEnumerable<string> elements)
    {
        return elements.Cast<object?>().ToArray();
    }

    public static Array CastToGenericArray(this IEnumerable<bool> elements)
    {
        return elements.Cast<object?>().ToArray();
    }

    public static List<object?> CastToGenericList(this IEnumerable<int> elements)
    {
        return elements.Cast<object?>().ToList();
    }

    public static List<object?> CastToGenericList(this IEnumerable<long> elements)
    {
        return elements.Cast<object?>().ToList();
    }

    public static List<object?> CastToGenericList(this IEnumerable<float> elements)
    {
        return elements.Cast<object?>().ToList();
    }

    public static List<object?> CastToGenericList(this IEnumerable<double> elements)
    {
        return elements.Cast<object?>().ToList();
    }

    public static List<object?> CastToGenericList(this IEnumerable<decimal> elements)
    {
        return elements.Cast<object?>().ToList();
    }

    public static List<object?> CastToGenericList(this IEnumerable<string> elements)
    {
        return elements.Cast<object?>().ToList();
    }

    public static List<object?> CastToGenericList(this IEnumerable<bool> elements)
    {
        return elements.Cast<object?>().ToList();
    }

    public static IEnumerable<T> CastToList<T>(this IEnumerable<object?> elements)
    {
        return elements.Cast<T>().ToList();
    }

    /// <summary>
    ///     Foreach with an index.
    /// </summary>
    /// <param name="ie">IEnumerable element</param>
    /// <param name="action">
    ///     Action to do for each loop.
    ///     Action receives a (int, T) tuple containing the index and the current element in the loop as a parameter
    /// </param>
    /// <typeparam name="T">Type of input.</typeparam>
    public static void Each<T>(this IEnumerable<T> ie, Action<int, T> action)
    {
        var i = 0;
        foreach (var e in ie) action(i++, e);
    }
    
    public static object? GetMiddleElement(this IEnumerable<object?> elements, bool useUpperIndexIfEven = false)
    {
        var enumerable = elements as object?[] ?? elements.ToArray();

        var index = enumerable.GetMiddleIndex(useUpperIndexIfEven);

        return enumerable.GetValue(index);
    }

    public static object? GetMiddleElement(this Array elements, bool useUpperIndexIfEven = false)
    {
        var index = elements.GetMiddleIndex(useUpperIndexIfEven);

        return elements.GetValue(index);
    }

    public static int GetMiddleIndex(this Array elements, bool useUpperIndexIfEven = false)
    {
        var length = elements.Length;

        if (length == 1) return 0;

        int middleIndex;

        if (length.IsOdd())
            middleIndex = (int)(System.Math.Floor(length / 2.0) + 1);
        else
            middleIndex = (int)(System.Math.Floor(length / 2.0) + (useUpperIndexIfEven ? 1 : 0));

        return middleIndex - 1; // -1 because arrays are 0-indexed
    }
}
