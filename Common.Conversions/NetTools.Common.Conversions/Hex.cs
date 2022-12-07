namespace NetTools.Common.Conversions;

public static class Hex
{
    /// <summary>
    ///     Convert a hex string array to a base64 string
    /// </summary>
    /// <param name="hexString">Hex string to convert to base64 string.</param>
    /// <returns>Base64 string</returns>
    public static string HexToBase64String(this string hexString)
    {
        var byteArray = HexToByteArray(hexString);
        return byteArray.BytesToBase64String();
    }

    /// <summary>
    ///     Convert a hex string to a byte array
    /// </summary>
    /// <param name="hexString">Hex string to convert to a byte array.</param>
    /// <returns>Byte array</returns>
    public static byte[] HexToByteArray(this string hexString)
    {
        return Enumerable.Range(0, hexString.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
            .ToArray();
    }

    /// <summary>
    ///     Construct a lookup table of hex values.
    /// </summary>
    /// <returns>Lookup table of hex values</returns>
    internal static uint[] CreateLookup32()
    {
        var result = new uint[256];
        for (var i = 0; i < 256; i++)
        {
            var s = i.ToString("X2");
            result[i] = (uint)s[0] + ((uint)s[1] << 16);
        }

        return result;
    }
}
