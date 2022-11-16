using System;
using System.Collections.Generic;

namespace NetTools;

/*
 * This class houses all the extension methods (methods whose first parameter is preceded by the "this" keyword)
 * https://stackoverflow.com/a/846773
 *
 * This class must be static, but will not need to be referenced by name (the class name does not matter)
 * e.g. a user will call "myString.ToTitleCase()" instead of "General.ToTitleCase(myString)"
 */
public static class ExtensionMethods
{
    /// <summary>
    ///     Foreach with an index.
    /// </summary>
    /// <param name="ie">IEnumerable element</param>
    /// <param name="action">Action to do for each loop.
    /// Action receives a (int, T) tuple containing the index and the current element in the loop as a parameter</param>
    /// <typeparam name="T">Type of input.</typeparam>
    public static void Each<T>(this IEnumerable<T> ie, Action<int, T> action)
    {
        var i = 0;
        foreach (var e in ie) action(i++, e);
    }

    public static bool AtLeastOne(this IEnumerable<object?> elements, Func<object?, bool> check)
    {
        return Validation.AtLeastOne(check, elements);
    }
    
    public static bool AtMostOne(this IEnumerable<object?> elements, Func<object?, bool> check)
    {
        return Validation.AtMostOne(check, elements);
    }
    
    public static bool ExactlyOne(this IEnumerable<object?> elements, Func<object?, bool> check)
    {
        return Validation.ExactlyOne(check, elements);
    }

    public static bool AtLeast(this IEnumerable<object?> elements, int number, Func<object?, bool> check)
    {
        return Validation.AtLeast(number, check, elements);
    }
    
    public static bool AtMost(this IEnumerable<object?> elements, int number, Func<object?, bool> check)
    {
        return Validation.AtMost(number, check, elements);
    }
    
    public static bool Exactly(this IEnumerable<object?> elements, int number, Func<object?, bool> check)
    {
        return Validation.Exactly(number, check, elements);
    }
    
    public static bool AtLeastOneExists(this IEnumerable<object?> elements)
    {
        return Validation.AtLeastOneExists(elements);
    }
    
    public static bool AtMostOneExists(this IEnumerable<object?> elements)
    {
        return Validation.AtMostOneExists(elements);
    }
    
    public static bool ExactlyOneExists(this IEnumerable<object?> elements)
    {
        return Validation.ExactlyOneExists(elements);
    }
    
    public static bool AtLeastOneDoesNotExist(this IEnumerable<object?> elements)
    {
        return Validation.AtLeastOneDoesNotExist(elements);
    }
    
    public static bool AtMostOneDoesNotExist(this IEnumerable<object?> elements)
    {
        return Validation.AtMostOneDoesNotExist(elements);
    }
    
    public static bool ExactlyOneDoesNotExist(this IEnumerable<object?> elements)
    {
        return Validation.ExactlyOneDoesNotExist(elements);
    }
    
    public static bool AtLeastXExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.AtLeastXExist(x, elements);
    }
    
    public static bool AtMostXExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.AtMostXExist(x, elements);
    }
    
    public static bool ExactlyXExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.ExactlyXExist(x, elements);
    }
    
    public static bool AtLeastXDoNotExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.AtLeastXDoNotExist(x, elements);
    }
    
    public static bool AtMostXDoNotExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.AtMostXDoNotExist(x, elements);
    }
    
    public static bool ExactlyXDoNotExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.ExactlyXDoNotExist(x, elements);
    }
    
    public static bool AnyExist(this IEnumerable<object?> elements)
    {
        return Validation.AnyExist(elements);
    }
    
    public static bool AnyDoNotExist(this IEnumerable<object?> elements)
    {
        return Validation.AnyDoNotExist(elements);
    }
    
    public static bool AllExist(this IEnumerable<object?> elements)
    {
        return Validation.AllExist(elements);
    }
    
    public static bool NoneExist(this IEnumerable<object?> elements)
    {
        return Validation.NoneExist(elements);
    }
}
