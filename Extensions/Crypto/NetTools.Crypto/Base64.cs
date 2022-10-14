namespace NetTools.Crypto;

public static class Base64
{
    private const string Base64PrefixRegexPattern = "^(data:)([a-zA-Z0-9]+/[a-zA-Z0-9]+;)(base64,)";

    public static byte[] ToByteArray(string base64String)
    {
        var prefixStripped = NetTools.RegularExpressions.Replace(base64String, Base64PrefixRegexPattern, string.Empty, true);
        return Convert.FromBase64String(prefixStripped);
    }
}
