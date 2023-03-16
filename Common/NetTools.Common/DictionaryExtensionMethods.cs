using System.Collections.Generic;

namespace NetTools.Common;

public static class DictionaryExtensionMethods
{
    /// <summary>
        ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object? (nullable) key-value pairs to a dictionary of string, object key-value pairs
        ///     by omitting key-value pairs with null values.
        /// </summary>
        /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object pairs.</returns>
        public static Dictionary<string, object> ToStringNonNullableObjectDictionary(this Dictionary<string, object?> dictionary) => Dictionaries.ConvertToStringNonNullableObjectDictionary(dictionary);

        /// <summary>
        ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object key-value pairs to a dictionary of string, object? (nullable) key-value pairs.
        /// </summary>
        /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object? pairs.</returns>
        public static Dictionary<string, object?> ToStringNullableObjectDictionary(this Dictionary<string, object> dictionary) => Dictionaries.ConvertToStringNullableObjectDictionary(dictionary);

        /// <summary>
        ///     Wrap a dictionary into a larger dictionary.
        ///     i.e. add a dictionary of parameters to "level1" -> "level2" -> "level3" -> "parameters".
        /// </summary>
        /// <param name="dictionary">Dictionary to wrap.</param>
        /// <param name="keys">Path of keys to wrap the parameters in.</param>
        /// <returns>A wrapped dictionary.</returns>
        internal static Dictionary<string, object> Wrap(this Dictionary<string, object> dictionary, params string[] keys) => Dictionaries.Wrap(dictionary, keys);

        /// <summary>
        ///     Wrap a list into a larger dictionary.
        ///     i.e. add a list of parameters to "level1" -> "level2" -> "level3" -> "parameters".
        /// </summary>
        /// <param name="list">List to wrap.</param>
        /// <param name="keys">Path of keys to wrap the parameters in.</param>
        /// <typeparam name="T">Type of list elements.</typeparam>
        /// <returns>A wrapped dictionary.</returns>
        internal static Dictionary<string, object> Wrap<T>(this List<T> list, params string[] keys) => Dictionaries.Wrap(list, keys);
        
        /// <summary>
        ///     Get a value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <typeparam name="T">The type of value to extract.</typeparam>
        /// <returns>A T-type object, or null if key does not exist.</returns>
        internal static T? GetOrNull<T>(this Dictionary<string, object> dictionary, string key) where T : class => Dictionaries.GetOrNull<T>(dictionary, key);

        /// <summary>
        ///     Get a value from a dictionary, or the default if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <typeparam name="T">The type of value to extract.</typeparam>
        /// <returns>A T-type object, or default if key does not exist.</returns>
        internal static T? GetOrDefault<T>(this Dictionary<string, object> dictionary, string key) => Dictionaries.GetOrDefault<T>(dictionary, key);

        /// <summary>
        ///     Get a boolean value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A boolean, or null if key does not exist.</returns>
        internal static bool? GetOrNullBoolean(this Dictionary<string, object> dictionary, string key) => Dictionaries.GetOrNullBoolean(dictionary, key);

        /// <summary>
        ///     Get a double value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A double, or null if key does not exist.</returns>
        internal static double? GetOrNullDouble(this Dictionary<string, object> dictionary, string key) => Dictionaries.GetOrNullDouble(dictionary, key);

        /// <summary>
        ///     Get an int value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>An int, or null if key does not exist.</returns>
        internal static int? GetOrNullInt(this Dictionary<string, object> dictionary, string key) => Dictionaries.GetOrNullInt(dictionary, key);

        /// <summary>
        ///     Get a float value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A float, or null if key does not exist.</returns>
        internal static float? GetOrNullFloat(this Dictionary<string, object> dictionary, string key) => Dictionaries.GetOrNullFloat(dictionary, key);

        /// <summary>
        ///     Get a long value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A long, or null if key does not exist.</returns>
        internal static long? GetOrNullLong(this Dictionary<string, object> dictionary, string key) => Dictionaries.GetOrNullLong(dictionary, key);
}
