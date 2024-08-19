using System.Text;

namespace NetTools.HTTP;

public interface IAuthentication
{
    public bool IsHeaderBased { get; }

    public bool IsQueryParameterBased { get; }

    public Dictionary<string, string> AuthenticationPair { get; }
}

public abstract class HeaderAuthentication : IAuthentication
{
    public bool IsHeaderBased => true;

    public bool IsQueryParameterBased => false;

    private string Key { get; set; }

    private string Value { get; set; }

    protected HeaderAuthentication(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public Dictionary<string, string> AuthenticationPair => new() { { Key, Value } };
}

public abstract class ApiKeyAuthentication : HeaderAuthentication
{
    protected ApiKeyAuthentication(string apiKey, string type) : base("Authorization", $"{type} {apiKey}")
    {
    }
}

public class BearerApiKeyAuthentication : ApiKeyAuthentication
{
    public BearerApiKeyAuthentication(string apiKey) : base(apiKey, "Bearer")
    {
    }
}

public class BasicApiKeyAuthentication : ApiKeyAuthentication
{
    public BasicApiKeyAuthentication(string apiKey) : base(apiKey, "Basic")
    {
    }
}

public class UsernamePasswordAuthentication : HeaderAuthentication
{
    public UsernamePasswordAuthentication(string username, string password) : base("Authorization",
        $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))}")
    {
    }
}

public class TokenAuthentication : HeaderAuthentication
{
    public TokenAuthentication(string token) : base("Authorization", $"Bearer {token}")
    {
    }
}

public class QueryParameterAuthentication : IAuthentication
{
    public bool IsHeaderBased => false;

    public bool IsQueryParameterBased => true;

    private string Key { get; set; }

    private string Value { get; set; }

    public QueryParameterAuthentication(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public Dictionary<string, string> AuthenticationPair => new()
    {
        { Key, Value }
    };
}