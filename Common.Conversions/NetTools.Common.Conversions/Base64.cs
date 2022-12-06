namespace NetTools.Common.Conversions;

public static class Base64
{
    private const string Base64PrefixRegexPattern = "^(data:)([a-zA-Z0-9]+/[a-zA-Z0-9]+;)(base64,)";

    /// <summary>
    ///     Convert a base64 string to a byte array
    /// </summary>
    /// <param name="base64String">Base64 string to convert to a byte array.</param>
    /// <returns>Byte array</returns>
    public static byte[] Base64ToByteArray(this string base64String)
    {
        var prefixStripped = NetTools.RegularExpressions.Replace(base64String, Base64PrefixRegexPattern, string.Empty, true);
        return Convert.FromBase64String(prefixStripped);
    }
    
    /// <summary>
    ///     Convert a base64 string to a hex string
    /// </summary>
    /// <param name="base64String">Base64 string to convert to a hex string.</param>
    /// <returns>Hex string</returns>
    public static string Base64ToHexString(this string base64String)
    {
        var bytes = Base64ToByteArray(base64String);
        return bytes.BytesToHexString();
    }
}
