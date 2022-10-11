using System.Globalization;
using System.Text.RegularExpressions;

namespace NetTools;

public static class RegularExpressions
{
    /// <summary>
    ///     Check if a matching substring exists in a string
    /// </summary>
    /// <param name="pattern">Pattern to search for.</param>
    /// <param name="text">Text to search within.</param>
    /// <returns>True if a matching substring exists, false otherwise.</returns>
    public static bool MatchExists(string pattern, string text)
    {
        var rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var matches = rx.Matches(text);
        return matches.Count > 0;
    }

    /// <summary>
    ///     Check if the given string matches the given pattern.
    /// </summary>
    /// <param name="input">String to check.</param>
    /// <param name="pattern">Pattern to match against.</param>
    /// <param name="ignoreCase">Whether to ignore case when evaluating string.</param>
    /// <returns>True if the string matches the pattern, false otherwise.</returns>
    public static bool Matches(string input, string pattern, bool ignoreCase = true)
    {
        try
        {
            return Regex.IsMatch(input,
                pattern,
                (ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.ExplicitCapture),
                TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
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
