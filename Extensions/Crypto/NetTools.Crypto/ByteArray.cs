namespace NetTools.Crypto;

public static class ByteArray
{
    public static string ToBase64String(byte[] byteArray)
    {
        return Convert.ToBase64String(byteArray);
    }
}