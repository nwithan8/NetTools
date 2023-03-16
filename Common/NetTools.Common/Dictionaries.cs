using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace NetTools.Common;

public static class Dictionaries
{
    /// <summary>
    ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object? (nullable) key-value pairs to a dictionary of string, object key-value pairs
    ///     by omitting key-value pairs with null values.
    /// </summary>
    /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object pairs.</returns>
    public static Dictionary<string, object> ConvertToStringNonNullableObjectDictionary(Dictionary<string, object?> dictionary)
    {
        var newDictionary = new Dictionary<string, object>();
        foreach (var item in dictionary)
        {
            if (item.Value != null)
            {
                newDictionary.Add(item.Key, item.Value);
            }
        }

        return newDictionary;
    }

    /// <summary>
    ///     Converts a <see cref="Dictionary{TKey,TValue}"/> of string, object key-value pairs to a dictionary of string, object? (nullable) key-value pairs.
    /// </summary>
    /// <param name="dictionary">A <see cref="Dictionary{TKey,TValue}"/> to convert.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of string, object? pairs.</returns>
    public static Dictionary<string, object?> ConvertToStringNullableObjectDictionary(Dictionary<string, object> dictionary)
    {
        var newDictionary = new Dictionary<string, object?>();
        foreach (var item in dictionary)
        {
            newDictionary.Add(item.Key, item.Value);
        }

        return newDictionary;
    }

    /// <summary>
    ///     Wrap a dictionary into a larger dictionary.
    ///     i.e. add a dictionary of parameters to "level1" -> "level2" -> "level3" -> "parameters".
    /// </summary>
    /// <param name="dictionary">Dictionary to wrap.</param>
    /// <param name="keys">Path of keys to wrap the parameters in.</param>
    /// <returns>A wrapped dictionary.</returns>
    internal static Dictionary<string, object> Wrap(Dictionary<string, object> dictionary, params string[] keys) => keys.Reverse().Aggregate(dictionary, (current, key) => new Dictionary<string, object> { { key, current } });

    /// <summary>
    ///     Wrap a list into a larger dictionary.
    ///     i.e. add a list of parameters to "level1" -> "level2" -> "level3" -> "parameters".
    /// </summary>
    /// <param name="list">List to wrap.</param>
    /// <param name="keys">Path of keys to wrap the parameters in.</param>
    /// <typeparam name="T">Type of list elements.</typeparam>
    /// <returns>A wrapped dictionary.</returns>
    internal static Dictionary<string, object> Wrap<T>(List<T> list, params string[] keys)
    {
        var firstKey = keys.Reverse().First();
        Dictionary<string, object> dictionary = new() { { firstKey, list } };
        return keys.Reverse().Skip(1).Aggregate(dictionary, (current, key) => new Dictionary<string, object> { { key, current } });
    }
    
    /// <summary>
        ///     Get a value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <typeparam name="T">The type of value to extract.</typeparam>
        /// <returns>A T-type object, or null if key does not exist.</returns>
        internal static T? GetOrNull<T>(Dictionary<string, object> dictionary, string key) where T : class
        {
            if (!dictionary.TryGetValue(key, out var value)) return null;
            return value switch
            {
                T t => t,
                JObject jObject => jObject.ToObject<T>(),
                JArray jArray => jArray.ToObject<T>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get a value from a dictionary, or the default if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <typeparam name="T">The type of value to extract.</typeparam>
        /// <returns>A T-type object, or default if key does not exist.</returns>
        internal static T? GetOrDefault<T>(Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value)) return default;
            return value switch
            {
                T t => t,
                JObject jObject => jObject.ToObject<T>(),
                JArray jArray => jArray.ToObject<T>(),
                var _ => default,
            };
        }

        /// <summary>
        ///     Get a boolean value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A boolean, or null if key does not exist.</returns>
        internal static bool? GetOrNullBoolean(Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value)) return null;
            return value switch
            {
                bool b => b,
                JObject jObject => jObject.ToObject<bool>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get a double value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A double, or null if key does not exist.</returns>
        internal static double? GetOrNullDouble(Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value)) return null;
            return value switch
            {
                double d => d,
                float f => (double)f,
                int i => (double)i,
                long l => (double)l,
                string s => double.TryParse(s, out var d) ? d : null,
                JObject jObject => jObject.ToObject<double>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get an int value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>An int, or null if key does not exist.</returns>
        internal static int? GetOrNullInt(Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value)) return null;
            return value switch
            {
                int i => i,
                long l => (int)l,
                float f => (int)f,
                double d => (int)d,
                string s => int.TryParse(s, out var i) ? i : null,
                JObject jObject => jObject.ToObject<int>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get a float value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A float, or null if key does not exist.</returns>
        internal static float? GetOrNullFloat(Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value)) return null;
            return value switch
            {
                float f => f,
                double d => (float)d,
                int i => (float)i,
                long l => (float)l,
                string s => float.TryParse(s, out var f) ? f : null,
                JObject jObject => jObject.ToObject<float>(),
                var _ => null,
            };
        }

        /// <summary>
        ///     Get a long value from a dictionary, or null if the key does not exist.
        /// </summary>
        /// <param name="dictionary">The dictionary to extract the value from.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>A long, or null if key does not exist.</returns>
        internal static long? GetOrNullLong(Dictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value)) return null;
            return value switch
            {
                long l => l,
                int i => (long)i,
                float f => (long)f,
                double d => (long)d,
                string s => long.TryParse(s, out var l) ? l : null,
                JObject jObject => jObject.ToObject<long>(),
                var _ => null,
            };
        }
}
