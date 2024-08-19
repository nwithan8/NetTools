namespace NetTools.JSON;

/// <summary>
///     A JSON serializer instance with internal de/serialization settings
/// </summary>
public class JsonSerializer
{
    protected readonly Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings;

    /// <summary>
    ///     Constructor for a new JsonSerializer instance.
    /// </summary>
    /// <param name="jsonSerializerSettings"><see cref="Newtonsoft.Json.JsonSerializerSettings" /> to be used by this instance.</param>
    public JsonSerializer(Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null)
    {
        JsonSerializerSettings = jsonSerializerSettings ?? new Newtonsoft.Json.JsonSerializerSettings
        {
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore
        };
    }

    /// <summary>
    ///     Deserialize data from a <see cref="HttpResponseMessage"/> into a T-type object.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/> object to extract data from.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <typeparam name="T">Type of object to deserialize to.</typeparam>
    /// <returns>A T-type object.</returns>
    public async Task<T> ConvertJsonToObject<T>(HttpResponseMessage response, List<string>? rootElementKeys = null)
    {
        return await JsonSerialization.ConvertJsonToObject<T>(response, JsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Deserialize data from a <see cref="HttpContent"/> into a T-type object.
    /// </summary>
    /// <param name="content"><see cref="HttpContent"/> object to extract data from.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <typeparam name="T">Type of object to deserialize to.</typeparam>
    /// <returns>A T-type object.</returns>
    public async Task<T> ConvertJsonToObject<T>(HttpContent content, List<string>? rootElementKeys = null)
    {
        return JsonSerialization.ConvertJsonToObject<T>(await content.ReadAsStringAsync(), JsonSerializerSettings,
            rootElementKeys);
    }

    /// <summary>
    ///     Deserialize a JSON string into a T-type object.
    /// </summary>
    /// <param name="data">A string of JSON data.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <typeparam name="T">Type of object to deserialize to.</typeparam>
    /// <returns>A T-type object.</returns>
    public T ConvertJsonToObject<T>(string? data, List<string>? rootElementKeys = null)
    {
        return JsonSerialization.ConvertJsonToObject<T>(data, JsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Deserialize data from a <see cref="HttpResponseMessage"/> into a specified type object.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/> object to extract data from.</param>
    /// <param name="type">Type of object to deserialize to.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>A <c>type</c>-type object.</returns>
    public async Task<object> ConvertJsonToObject(HttpResponseMessage response, Type type,
        List<string>? rootElementKeys = null)
    {
        return await JsonSerialization.ConvertJsonToObject(response, type, JsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Deserialize data from a <see cref="HttpContent"/> into a specified type object.
    /// </summary>
    /// <param name="content"><see cref="HttpContent"/> object to extract data from.</param>
    /// <param name="type">Type of object to deserialize to.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>A <c>type</c>-type object.</returns>
    public async Task<object> ConvertJsonToObject(HttpContent content, Type type, List<string>? rootElementKeys = null)
    {
        return await JsonSerialization.ConvertJsonToObject(content, type, JsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Deserialize a JSON string into a specified type object.
    /// </summary>
    /// <param name="data">A string of JSON data.</param>
    /// <param name="type">Type of object to deserialize to.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>A <c>type</c>-type object.</returns>
    public object ConvertJsonToObject(string? data, Type type, List<string>? rootElementKeys = null)
    {
        return JsonSerialization.ConvertJsonToObject(data, JsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Deserialize a JSON string into a dynamic object.
    /// </summary>
    /// <param name="data">A string of JSON data.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>An ExpandoObject object.</returns>
    public System.Dynamic.ExpandoObject ConvertJsonToObject(string? data, List<string>? rootElementKeys = null)
    {
        return JsonSerialization.ConvertJsonToObject(data, JsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Serialize an object into a JSON string.
    /// </summary>
    /// <param name="data">An object to serialize into a string.</param>
    /// <returns>A string of JSON data.</returns>
    public string ConvertObjectToJson(object data)
    {
        return JsonSerialization.ConvertObjectToJson(data, JsonSerializerSettings);
    }
}

/// <summary>
///     JSON de/serialization utilities
/// </summary>
public static class JsonSerialization
{
    /// <summary>
    ///     The default <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for de/serialization
    /// </summary>
    private static Newtonsoft.Json.JsonSerializerSettings DefaultJsonSerializerSettings => new()
    {
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore,
        DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
        DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc
    };

    /// <summary>
    ///     Deserialize data from a <see cref="HttpResponseMessage"/> into a T-type object, using this instance's
    ///     <see cref="Newtonsoft.Json.JsonSerializerSettings" />.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/> object to extract data from.</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <typeparam name="T">Type of object to deserialize to.</typeparam>
    /// <returns>A T-type object.</returns>
    public static async Task<T> ConvertJsonToObject<T>(HttpResponseMessage response,
        Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) =>
        await ConvertJsonToObject<T>(response.Content, jsonSerializerSettings, rootElementKeys);

    /// <summary>
    ///     Deserialize data from a <see cref="HttpContent"/> into a T-type object, using this instance's
    ///     <see cref="Newtonsoft.Json.JsonSerializerSettings" />.
    /// </summary>
    /// <param name="content"><see cref="HttpContent"/> object to extract data from.</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <typeparam name="T">Type of object to deserialize to.</typeparam>
    /// <returns>A T-type object.</returns>
    public static async Task<T> ConvertJsonToObject<T>(HttpContent content,
        Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null) =>
        ConvertJsonToObject<T>(await content.ReadAsStringAsync(), jsonSerializerSettings, rootElementKeys);

    /// <summary>
    ///     Deserialize a JSON string into a T-type object
    /// </summary>
    /// <param name="data">A string of JSON data</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <typeparam name="T">Type of object to deserialize to</typeparam>
    /// <returns>A T-type object</returns>
    public static T ConvertJsonToObject<T>(string? data,
        Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
    {
        var obj = ConvertJsonToObject(data, typeof(T), jsonSerializerSettings, rootElementKeys);
        if (obj is T t) return t;

        throw new JsonDeserializationException(typeof(T));
    }

    /// <summary>
    ///     Deserialize data from a <see cref="HttpResponseMessage"/> into a specified type object, using this instance's <see cref="Newtonsoft.Json.JsonSerializerSettings" />.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/> object to extract data from.</param>
    /// <param name="type">Type of object to deserialize to.</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>A <c>type</c>-type object.</returns>
    public static async Task<object> ConvertJsonToObject(HttpResponseMessage response, Type type,
        Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
    {
        return await ConvertJsonToObject(response.Content, type, jsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Deserialize data from a <see cref="HttpContent"/> into a specified type object, using this instance's <see cref="Newtonsoft.Json.JsonSerializerSettings" />.
    /// </summary>
    /// <param name="content"><see cref="HttpContent"/> object to extract data from.</param>
    /// <param name="type">Type of object to deserialize to.</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>A <c>type</c>-type object.</returns>
    public static async Task<object> ConvertJsonToObject(HttpContent content, Type type,
        Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
    {
        return ConvertJsonToObject(await content.ReadAsStringAsync(), type, jsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Deserialize a JSON string into a specified type object, using this instance's <see cref="Newtonsoft.Json.JsonSerializerSettings" />.
    /// </summary>
    /// <param name="data">A string of JSON data.</param>
    /// <param name="type">Type of object to deserialize to.</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>A <c>type</c>-type object.</returns>
    public static object ConvertJsonToObject(string? data, Type type,
        Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
    {
        if (rootElementKeys != null && rootElementKeys.Any()) data = GoToRootElement(data, rootElementKeys);

        if (data == null || string.IsNullOrWhiteSpace(data)) throw new JsonNoDataException();

        try
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(data, type,
                jsonSerializerSettings ?? DefaultJsonSerializerSettings);
            return (obj ?? default)!;
        }
        catch (Exception)
        {
            throw new JsonDeserializationException(type);
        }
    }

    /// <summary>
    ///     Deserialize a JSON string into a dynamic object, using this instance's <see cref="Newtonsoft.Json.JsonSerializerSettings" />.
    /// </summary>
    /// <param name="data">A string of JSON data.</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     deserialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>An ExpandoObject object.</returns>
    public static System.Dynamic.ExpandoObject ConvertJsonToObject(string? data,
        Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null, List<string>? rootElementKeys = null)
    {
        return ConvertJsonToObject<System.Dynamic.ExpandoObject>(data, jsonSerializerSettings, rootElementKeys);
    }

    /// <summary>
    ///     Serialize an object into a JSON string, using this instance's <see cref="Newtonsoft.Json.JsonSerializerSettings" />
    /// </summary>
    /// <param name="data">An object to serialize into a string</param>
    /// <param name="jsonSerializerSettings">
    ///     The <see cref="Newtonsoft.Json.JsonSerializerSettings" /> to use for
    ///     serialization. Defaults to <see cref="DefaultJsonSerializerSettings" /> if not provided.
    /// </param>
    /// <returns>A string of JSON data.</returns>
    public static string ConvertObjectToJson(object data,
        Newtonsoft.Json.JsonSerializerSettings? jsonSerializerSettings = null)
    {
        try
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data,
                jsonSerializerSettings ?? DefaultJsonSerializerSettings);
        }
        catch (Exception)
        {
            throw new JsonSerializationException(data.GetType());
        }
    }

    /// <summary>
    ///     Venture through the root element keys to find the root element of the JSON string.
    /// </summary>
    /// <param name="data">A string of JSON data.</param>
    /// <param name="rootElementKeys">List, in order, of sub-keys path to follow to deserialization starting position.</param>
    /// <returns>The value of the JSON sub-element key path.</returns>
    private static string? GoToRootElement(string? data, List<string> rootElementKeys)
    {
        if (data == null) return null;

        var json = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
        try
        {
            rootElementKeys.ForEach(key => { json = (json as Newtonsoft.Json.Linq.JObject)?.Property(key)?.Value; });
            return (json as Newtonsoft.Json.Linq.JToken)?.ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }
}