using NetTools.Common;
using NetTools.Common.Conversions;

namespace NetTools.Common.Files;

public static class Files
{
    public static void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }

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

    public static void SaveBase64AsFile(string base64String, string path)
    {
        var byteArray = base64String.Base64ToByteArray();
        SaveByteArrayAsFile(byteArray, path);
    }

    public static void SaveByteArrayAsFile(byte[] byteArray, string path)
    {
        using var stream = new FileStream(path, FileMode.Create);
        stream.Write(byteArray, 0, byteArray.Length);
        stream.Flush();
    }
}
