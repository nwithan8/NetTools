using System.Text;

namespace NetTools.Crypto;

internal static class Statics
{
    /// <summary>
    ///     Construct a lookup table of hex values.
    /// </summary>
    /// <returns>Lookup table of hex values</returns>
    private static uint[] CreateLookup32()
    {
        var result = new uint[256];
        for (var i = 0; i < 256; i++)
        {
            var s = i.ToString("X2");
            result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
        }

        return result;
    }

    /// <summary>
    ///     Convert a string to a byte array using a specific encoding (defaults to UTF-8)
    /// </summary>
    /// <param name="str">String to convert to byte array.</param>
    /// <param name="encoding">Encoding to use. Default: UTF-8</param>
    /// <returns>Byte array</returns>
    internal static byte[] AsByteArray(this string str, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        return encoding.GetBytes(str);
    }

    /// <summary>
    ///     Convert a byte array to a hex string.
    /// </summary>
    /// <param name="bytes">Byte array to convert to hex string.</param>
    /// <returns>Hex string</returns>
    internal static string AsHexString(this IReadOnlyList<byte> bytes)
    {
        // Fastest safe way to convert a byte array to hex string,
        // per https://stackoverflow.com/a/624379/13343799

        var lookup32 = CreateLookup32();
        var result = new char[bytes.Count * 2];
        for (var i = 0; i < bytes.Count; i++)
        {
            var val = lookup32[bytes[i]];
            result[2 * i] = (char)val;
            result[2 * i + 1] = (char)(val >> 16);
        }

        return new string(result).ToLower();
    }

    /// <summary>
    ///     Convert a string to a hex string using a specific encoding (defaults to UTF-8)
    /// </summary>
    /// <param name="str">String to convert to hex string.</param>
    /// <param name="encoding">Encoding to use. Default: UTF-8</param>
    /// <returns>Hex string</returns>
    internal static string AsHexString(this string str, Encoding? encoding = null)
    {
        var bytes = str.AsByteArray(encoding);

        return bytes.AsHexString();
    }

    /// <summary>
    ///     Convert a byte array to a string using a specific encoding (defaults to UTF-8)
    /// </summary>
    /// <param name="bytes">Byte array to convert to string.</param>
    /// <param name="encoding">Encoding to use. Default: UTF-8</param>
    /// <returns>String</returns>
    internal static string AsString(this byte[] bytes, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        return encoding.GetString(bytes);
    }
}
