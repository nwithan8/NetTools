using System;
using System.Collections.Generic;
using System.Linq;

namespace NetTools.Common;

public static class DictionaryExtensionMethods
{
    /// <summary>
    ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object? (nullable) key-value pairs to a dictionary of string, object key-value pairs
    ///     by omitting key-value pairs with null values.
    /// </summary>
    /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object pairs.</returns>
    public static Dictionary<string, object>
        ToStringNonNullableObjectDictionary(this Dictionary<string, object?> dictionary) =>
        Dictionaries.ConvertToStringNonNullableObjectDictionary(dictionary);

    /// <summary>
    ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object key-value pairs to a dictionary of string, object? (nullable) key-value pairs.
    /// </summary>
    /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object? pairs.</returns>
    public static Dictionary<string, object?>
        ToStringNullableObjectDictionary(this Dictionary<string, object> dictionary) =>
        Dictionaries.ConvertToStringNullableObjectDictionary(dictionary);
    
    /// <summary>
        ///     Add a key-value pair to a dictionary if the key path does not exist, otherwise update the value.
        ///     This is a workaround for the fact that <see cref="IDictionary{TKey,TValue}"/> does not have an AddOrUpdate method.
        ///     This update runs in-place, so the dictionary is not copied and a new dictionary is not returned.
        /// </summary>
        /// <param name="dictionary">The dictionary to add/update the key-value pair in.</param>
        /// <param name="value">The value to add/update.</param>
        /// <param name="key">The key path to add/update.</param>
        public static void AddOrUpdate(this IDictionary<string, object?> dictionary, object? value, string key)
        {
            try
            {
                dictionary.Add(key, value);
            }
            catch (ArgumentException)
            {
                dictionary[key] = value;
            }
        }

        /// <summary>
        ///     Add a key-value pair to a dictionary if the key path does not exist, otherwise update the value.
        ///     This is a workaround for the fact that <see cref="IDictionary{TKey,TValue}"/> does not have an AddOrUpdate method.
        ///     This update runs in-place, so the dictionary is not copied and a new dictionary is not returned.
        /// </summary>
        /// <param name="dictionary">The dictionary to add/update the key-value pair in.</param>
        /// <param name="value">The value to add/update.</param>
        /// <param name="path">The key path to add/update.</param>
        public static void AddOrUpdate(this IDictionary<string, object?> dictionary, object? value, params string[] path)
        {
            switch (path.Length)
            {
                // Don't need to go down
                case 0:
                    return;

                // Last key left
                case 1:
                    dictionary.AddOrUpdate(value, path[0]);
                    return;

                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }

            // Need to go down another level
            // Get the key and update the list of keys
            string key = path[0];
            path = path.Skip(1).ToArray();
#pragma warning disable CA1854 // Don't want to use TryGetValue because no need for value
            if (!dictionary.ContainsKey(key))
            {
                var newDictionary = new Dictionary<string, object?>();
                newDictionary.AddOrUpdate(value, path);
                dictionary[key] = newDictionary;
            }
#pragma warning restore CA1854

            object? subDirectory = dictionary[key];
            if (subDirectory is Dictionary<string, object?> subDictionary)
            {
                subDictionary.AddOrUpdate(value, path);
                dictionary[key] = subDictionary;
            }
            else
            {
#pragma warning disable CA2201 // Don't throw base Exception class
                throw new Exception("Found a non-dictionary while traversing the dictionary");
#pragma warning restore CA2201 // Don't throw base Exception class
            }
        }

    /// <summary>
    ///     Wrap a dictionary into a larger dictionary.
    ///     i.e. add a dictionary of parameters to "level1" -> "level2" -> "level3" -> "parameters".
    /// </summary>
    /// <param name="dictionary">Dictionary to wrap.</param>
    /// <param name="keys">Path of keys to wrap the parameters in.</param>
    /// <returns>A wrapped dictionary.</returns>
    internal static Dictionary<string, object> Wrap(this Dictionary<string, object> dictionary, params string[] keys) =>
        Dictionaries.Wrap(dictionary, keys);

    /// <summary>
    ///     Wrap a list into a larger dictionary.
    ///     i.e. add a list of parameters to "level1" -> "level2" -> "level3" -> "parameters".
    /// </summary>
    /// <param name="list">List to wrap.</param>
    /// <param name="keys">Path of keys to wrap the parameters in.</param>
    /// <typeparam name="T">Type of list elements.</typeparam>
    /// <returns>A wrapped dictionary.</returns>
    internal static Dictionary<string, object> Wrap<T>(this List<T> list, params string[] keys) =>
        Dictionaries.Wrap(list, keys);
    
    /// <summary>
    ///     Merge another dictionary into this dictionary.
    /// </summary>
    /// <param name="dictionary">The base dictionary to add additional key-value pairs to.</param>
    /// <param name="other">The secondary dictionary to extract key-value pairs from.</param>
    /// <returns>The base dictionary with key-value pairs from the secondary dictionary added.</returns>
    internal static Dictionary<string, object> MergeIn(this Dictionary<string, object> dictionary, Dictionary<string, object> other)
    {
        foreach (KeyValuePair<string, object> item in other)
        {
            dictionary!.AddOrUpdate(item.Value, item.Key);
        }

        return dictionary;
    }

    /// <summary>
    ///     Get a value from a dictionary, or null if the key does not exist.
    /// </summary>
    /// <param name="dictionary">The dictionary to extract the value from.</param>
    /// <param name="key">The key to search for in the dictionary.</param>
    /// <typeparam name="T">The type of value to extract.</typeparam>
    /// <returns>A T-type object, or null if key does not exist.</returns>
    internal static T? GetOrNull<T>(this Dictionary<string, object> dictionary, string key) where T : class =>
        Dictionaries.GetOrNull<T>(dictionary, key);

    /// <summary>
    ///     Get a value from a dictionary, or the default if the key does not exist.
    /// </summary>
    /// <param name="dictionary">The dictionary to extract the value from.</param>
    /// <param name="key">The key to search for in the dictionary.</param>
    /// <typeparam name="T">The type of value to extract.</typeparam>
    /// <returns>A T-type object, or default if key does not exist.</returns>
    internal static T? GetOrDefault<T>(this Dictionary<string, object> dictionary, string key) =>
        Dictionaries.GetOrDefault<T>(dictionary, key);

    /// <summary>
    ///     Get a boolean value from a dictionary, or null if the key does not exist.
    /// </summary>
    /// <param name="dictionary">The dictionary to extract the value from.</param>
    /// <param name="key">The key to search for in the dictionary.</param>
    /// <returns>A boolean, or null if key does not exist.</returns>
    internal static bool? GetOrNullBoolean(this Dictionary<string, object> dictionary, string key) =>
        Dictionaries.GetOrNullBoolean(dictionary, key);

    /// <summary>
    ///     Get a double value from a dictionary, or null if the key does not exist.
    /// </summary>
    /// <param name="dictionary">The dictionary to extract the value from.</param>
    /// <param name="key">The key to search for in the dictionary.</param>
    /// <returns>A double, or null if key does not exist.</returns>
    internal static double? GetOrNullDouble(this Dictionary<string, object> dictionary, string key) =>
        Dictionaries.GetOrNullDouble(dictionary, key);

    /// <summary>
    ///     Get an int value from a dictionary, or null if the key does not exist.
    /// </summary>
    /// <param name="dictionary">The dictionary to extract the value from.</param>
    /// <param name="key">The key to search for in the dictionary.</param>
    /// <returns>An int, or null if key does not exist.</returns>
    internal static int? GetOrNullInt(this Dictionary<string, object> dictionary, string key) =>
        Dictionaries.GetOrNullInt(dictionary, key);

    /// <summary>
    ///     Get a float value from a dictionary, or null if the key does not exist.
    /// </summary>
    /// <param name="dictionary">The dictionary to extract the value from.</param>
    /// <param name="key">The key to search for in the dictionary.</param>
    /// <returns>A float, or null if key does not exist.</returns>
    internal static float? GetOrNullFloat(this Dictionary<string, object> dictionary, string key) =>
        Dictionaries.GetOrNullFloat(dictionary, key);

    /// <summary>
    ///     Get a long value from a dictionary, or null if the key does not exist.
    /// </summary>
    /// <param name="dictionary">The dictionary to extract the value from.</param>
    /// <param name="key">The key to search for in the dictionary.</param>
    /// <returns>A long, or null if key does not exist.</returns>
    internal static long? GetOrNullLong(this Dictionary<string, object> dictionary, string key) =>
        Dictionaries.GetOrNullLong(dictionary, key);

    /// <summary>
    ///     Get a value enum from a dictionary, or null if the key does not exist.
    /// </summary>
    /// <param name="dictionary">The dictionary to extract the value from.</param>
    /// <param name="key">The key to search for in the dictionary.</param>
    /// <typeparam name="T">The type of enum to extract.</typeparam>
    /// <returns>A T-type object, or default if key does not exist.</returns>
    internal static T? GetOrNullEnum<T>(this Dictionary<string, object> dictionary, string key) where T : ValueEnum =>
        Dictionaries.GetOrNullEnum<T>(dictionary, key);
}