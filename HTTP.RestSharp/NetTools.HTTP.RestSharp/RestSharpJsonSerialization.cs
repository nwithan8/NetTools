 using RestSharp;

namespace NetTools.HTTP.RestSharp;

/// <summary>
///     A JSON serializer instance with internal de/serialization settings
/// </summary>
public class JsonSerializer : NetTools.JSON.JsonSerializer
{
    /// <summary>
    ///     Deserialize data from a RestSharp.RestResponseBase into a T-type object, using this instance's
    ///     <see cref="JSON.JsonSerializer.JsonSerializerSettings" />
    /// </summary>
    /// <param name="response">RestSharp.RestResponseBase object to extract data from.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <typeparam name="T">Type of object to deserialize to</typeparam>
    /// <returns>A T-type object</returns>
    public T ConvertJsonToObject<T>(RestResponseBase response, List<string>? rootElementKeys = null)
    {
        return JsonSerialization.ConvertJsonToObject<T>(response, JsonSerializerSettings, rootElementKeys);
    }
}

/// <summary>
///     JSON de/serialization utilities
/// </summary>
public static class JsonSerialization
{
    /// <summary>
    ///     Deserialize data from a RestSharp.RestResponseBase into a T-type object, using this instance's
    ///     <see cref="JSON.JsonSerializer.JsonSerializerSettings" />
    /// </summary>
    /// <param name="response">RestSharp.RestResponseBase object to extract data from.</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     deserialization. Defaults to <see cref="JSON.JsonSerialization.DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <typeparam name="T">Type of object to deserialize to</typeparam>
    /// <returns>A T-type object</returns>
    public static T ConvertJsonToObject<T>(RestResponseBase response, Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
    {
        return JSON.JsonSerialization.ConvertJsonToObject<T>(response.Content, jsonSerializerSettings, rootElementKeys);
    }
}
