namespace NetTools.Files;

public static class Files
{
    public static byte[] ReadFile(string path)
    {
        using var fsSource = new FileStream(path,
            FileMode.Open, FileAccess.Read);
        // Read the source file into a byte array.
        var byteArray = new byte[fsSource.Length];
        var numBytesToRead = (int)fsSource.Length;
        var numBytesRead = 0;
        while (numBytesToRead > 0)
        {
            // Read may return anything from 0 to numBytesToRead.
            var n = fsSource.Read(byteArray, numBytesRead, numBytesToRead);

            // Break when the end of the file is reached.
            if (n == 0)
                break;

            numBytesRead += n;
            numBytesToRead -= n;
        }

        return byteArray;
    }

    public static void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }

    public static void SaveByteArrayAsFile(byte[] byteArray, string path)
    {
        using var stream = new FileStream(path, FileMode.Create);
        stream.Write(byteArray, 0, byteArray.Length);
        stream.Flush();
    }

    public static void SaveBase64AsFile(string base64String, string path)
    {
        var byteArray = NetTools.Crypto.Base64.ToByteArray(base64String: base64String);
        SaveByteArrayAsFile(byteArray: byteArray, path: path);
    }
}
