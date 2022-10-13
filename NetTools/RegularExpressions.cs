using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetTools;

public static class RegularExpressions
{
    private static RegexOptions GetDefaultOptions(bool ignoreCase = false)
    {
        return RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | (ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
    }

    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMilliseconds(250);

    /// <summary>
    ///     Check if the given string matches the given pattern.
    ///     Can be case-sensitive or case-insensitive.
    /// </summary>
    /// <param name="text">String to check.</param>
    /// <param name="pattern">Pattern to match against.</param>
    /// <param name="ignoreCase">Whether to ignore case when evaluating string.</param>
    /// <returns>True if the string matches the pattern, false otherwise.</returns>
    public static bool Matches(string text, string pattern, bool ignoreCase = true)
    {
        if (!Validation.Exists(text))
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(text,
                pattern,
                GetDefaultOptions(ignoreCase),
                DefaultTimeout);
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Get all matching substring in a string.
    ///     Case-sensitive.
    /// </summary>
    /// <param name="text">Text to search within.</param>
    /// <param name="pattern">Pattern to search for.</param>
    /// <param name="ignoreCase">Whether to ignore case when evaluating string.</param>
    /// <returns></returns>
    public static List<string> Substrings(string text, string pattern, bool ignoreCase = true)
    {
        var substrings = new List<string>();

        if (!Validation.Exists(text))
        {
            return substrings; // empty list
        }

        var rx = new Regex(pattern, GetDefaultOptions(ignoreCase));
        var matches = rx.Matches(text);
        foreach (Match match in matches)
        {
            substrings.Add(match.Value);
        }

        return substrings;
    }

    /// <summary>
    ///     Check if a matching substring exists in a string.
    ///     Case-sensitive.
    /// </summary>
    /// <param name="text">Text to search within.</param>
    /// <param name="pattern">Pattern to search for.</param>
    /// <param name="ignoreCase">Whether to ignore case when evaluating string.</param>
    /// <returns>True if a matching substring exists, false otherwise.</returns>
    public static bool SubstringExists(string text, string pattern, bool ignoreCase = true)
    {
        var matches = Substrings(text, pattern, ignoreCase);
        return matches.Count > 0;
    }

    /// <summary>
    ///     Replace all matching substrings in a string with a replacement string.
    /// </summary>
    /// <param name="text">Text to search within.</param>
    /// <param name="pattern">Pattern to search for.</param>
    /// <param name="ignoreCase">Whether to ignore case when evaluating string.</param>
    /// <param name="replacement">String to replace matches with.</param>
    /// <returns></returns>
    public static string Replace(string text, string pattern, string replacement, bool ignoreCase = true)
    {
        if (!Validation.Exists(text))
        {
            return string.Empty; // text is null or empty, return empty string
        }

        try
        {
            return Regex.Replace(text,
                pattern,
                replacement,
                GetDefaultOptions(ignoreCase),
                DefaultTimeout);
        }
        catch (RegexMatchTimeoutException)
        {
            return string.Empty;
        }
    }

    /// <summary>
    ///     Find all items with a specific string value for a property
    /// </summary>
    /// <param name="items">List of items to filter.</param>
    /// <param name="propertyName">Name of the property to check value of.</param>
    /// <param name="searchTerm">Value the property should have or contain.</param>
    /// <param name="ignoreCase">Whether to ignore case when comparing property values.</param>
    /// <typeparam name="T">Type of items to filter.</typeparam>
    /// <returns>A list of T-type objects.</returns>
    public static List<T> FilterByProperty<T>(List<T> items, string propertyName, string searchTerm, bool ignoreCase = true)
    {
        if (ignoreCase)
        {
            searchTerm = searchTerm.ToLower();
        }

        var filtered = new List<T>();
        foreach (var item in items)
        {
            var propertyValue = item?.GetType().GetProperty(propertyName)?.GetValue(item, null);

            if (propertyValue == null)
            {
                continue;
            }

            if (ignoreCase)
            {
                propertyValue = propertyValue.ToString()?.ToLower();
            }

            if (propertyValue!.ToString()!.Contains(searchTerm))
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
