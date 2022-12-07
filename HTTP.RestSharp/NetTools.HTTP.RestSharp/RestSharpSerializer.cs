using RestSharp;
using RestSharp.Serializers;

namespace NetTools.HTTP.RestSharp;

/// <summary>
///     A RestSharp-compatible serializer that uses <see cref="JsonSerialization" />.
///     RestSharp uses System.Text.Json by default.
///     This serializer will reroute the serialization process to <see cref="JsonSerialization" />, which uses
///     Newtonsoft.Json.
/// </summary>
public class RestSharpSerializer : IRestSerializer
{
    public string[] AcceptedContentTypes { get; } =
    {
        "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
    };
    public DataFormat DataFormat => DataFormat.Json;
    public IDeserializer Deserializer => new Deserializer();

    public ISerializer Serializer => new Serializer();
    public SupportsContentType SupportsContentType => type => AcceptedContentTypes.Contains(type);

    public string? Serialize(Parameter parameter)
    {
        // Override the System.Text.Json serializer that RestSharp used to use the Newtonsoft.Json serializer instead (via NetTools.HTTP)
        return parameter.Value != null ? JSON.JsonSerialization.ConvertObjectToJson(parameter.Value) : "";
    }
}

public class Serializer : ISerializer
{
    public string ContentType
    {
        get => "application/json";
        set { }
    }

    public string? Serialize(object obj)
    {
        // Override the System.Text.Json serializer that RestSharp used to use the Newtonsoft.Json serializer instead (via NetTools.HTTP)
        return JSON.JsonSerialization.ConvertObjectToJson(obj);
    }
}

public class Deserializer : IDeserializer
{
    public T? Deserialize<T>(RestResponse response)
    {
        // Override the System.Text.Json deserializer that RestSharp used to use the Newtonsoft.Json deserializer instead (via NetTools.HTTP)
        return JsonSerialization.ConvertJsonToObject<T>(response);
    }
}
