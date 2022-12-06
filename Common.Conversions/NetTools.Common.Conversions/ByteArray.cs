namespace NetTools.Common.Conversions;

public static class ByteArray
{
    /// <summary>
    ///     Convert a byte array to a base64 string
    /// </summary>
    /// <param name="byteArray">Byte array to convert to base64 string.</param>
    /// <returns>Base64 string</returns>
    public static string BytesToBase64String(this byte[] byteArray)
    {
        return Convert.ToBase64String(byteArray);
    }
    
    /// <summary>
    ///     Convert a byte array to a hex string.
    /// </summary>
    /// <param name="bytes">Byte array to convert to hex string.</param>
    /// <returns>Hex string</returns>
    public static string BytesToHexString(this IReadOnlyList<byte> bytes)
    {
        // Fastest safe way to convert a byte array to hex string,
        // per https://stackoverflow.com/a/624379/13343799

        var lookup32 = Hex.CreateLookup32();

        var result = new char[bytes.Count * 2];
        for (var i = 0; i < bytes.Count; i++)
        {
            var val = lookup32[bytes[i]];
            result[2 * i] = (char)val;
            result[2 * i + 1] = (char)(val >> 16);
        }

        return new string(result).ToLower();
    }
}