namespace NetTools.Conversions;

public static class Data
{
    public static class Base64
    {
        public static byte[] ToByteArray(string base64String)
        {
            base64String = base64String.Replace("data:image/jpeg;base64,", string.Empty); // TODO: genericize later
            return Convert.FromBase64String(base64String);
        }
    }

    public static class ByteArray
    {
        public static string ToBase64String(byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray);
        }
    }
}
