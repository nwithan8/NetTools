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
}
