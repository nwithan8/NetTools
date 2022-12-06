namespace NetTools.HTTP;

/// <summary>
///     A RestSharp-compatible serializer that uses <see cref="JsonSerialization" />.
///     RestSharp uses System.Text.Json by default.
///     This serializer will reroute the serialization process to <see cref="JsonSerialization" />, which uses Newtonsoft.Json.
/// </summary>
public class RestSharpSerializer : RestSharp.Serializers.IRestSerializer
{
    public string? Serialize(RestSharp.Parameter parameter)
    {
        // Override the System.Text.Json serializer that RestSharp used to use the Newtonsoft.Json serializer instead (via NetTools.HTTP)
        return parameter.Value != null ? JsonSerialization.ConvertObjectToJson(parameter.Value) : "";
    }

    public RestSharp.Serializers.ISerializer Serializer => new Serializer();
    public RestSharp.Serializers.IDeserializer Deserializer => new Deserializer();
    public string[] AcceptedContentTypes { get; } =
    {
        "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
    };
    public RestSharp.Serializers.SupportsContentType SupportsContentType => type => AcceptedContentTypes.Contains(type);
    public RestSharp.DataFormat DataFormat => RestSharp.DataFormat.Json;
}

public class Serializer : RestSharp.Serializers.ISerializer
{
    public string? Serialize(object obj)
    {
        // Override the System.Text.Json serializer that RestSharp used to use the Newtonsoft.Json serializer instead (via NetTools.HTTP)
        return JsonSerialization.ConvertObjectToJson(obj);
    }

    public string ContentType
    {
        get => "application/json";
        set { }
    }
}

public class Deserializer : RestSharp.Serializers.IDeserializer
{
    public T? Deserialize<T>(RestSharp.RestResponse response)
    {
        // Override the System.Text.Json deserializer that RestSharp used to use the Newtonsoft.Json deserializer instead (via NetTools.HTTP)
        return JsonSerialization.ConvertJsonToObject<T>(response);
    }
}
