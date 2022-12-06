using System.Security.Cryptography;
using System.Text;

namespace NetTools.Crypto;

public static class Encryption
{
    public static class Aes
    {
        private const int KeySize = 16;

        private static byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data is not { Length: > 0 })
            {
                throw new ArgumentNullException($"{nameof(data)} cannot be empty");
            }

            if (key is not { Length: KeySize })
            {
                throw new ArgumentException($"{nameof(key)} must be length of {KeySize}");
            }

            var aes = System.Security.Cryptography.Aes.Create();
            aes.Key = key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;


            aes.GenerateIV();
            var iv = aes.IV;
            using var encryptor = aes.CreateEncryptor(aes.Key, iv);
            using var cipherStream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(cipherStream, encryptor, CryptoStreamMode.Write))
            using (var binaryWriter = new BinaryWriter(cryptoStream))
            {
                binaryWriter.Write(data);
                cryptoStream.FlushFinalBlock();
            }

            var cipherBytes = cipherStream.ToArray();

            return iv.Concat(cipherBytes).ToArray();
        }

        private static byte[] Decrypt(byte[] data, byte[] key)
        {
            if (data is not { Length: > 0 })
            {
                throw new ArgumentNullException($"{nameof(data)} cannot be empty");
            }

            if (key is not { Length: KeySize })
            {
                throw new ArgumentException($"{nameof(key)} must be length of {KeySize}");
            }

            var aes = System.Security.Cryptography.Aes.Create();
            aes.Key = key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // get first KeySize bytes of data (IV) and use it to decrypt
            var iv = new byte[KeySize];
            Array.Copy(data, 0, iv, 0, iv.Length);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, iv),
                       CryptoStreamMode.Write))
            using (var binaryWriter = new BinaryWriter(cs))
            {
                // decrypt cipher text from data, starting just past the IV
                binaryWriter.Write(
                    data,
                    iv.Length,
                    data.Length - iv.Length
                );
            }

            var dataBytes = ms.ToArray();

            return dataBytes;
        }

        public static string EncryptString(string text, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            return EncryptString(text, keyBytes);
        }

        public static string DecryptString(string text, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            return DecryptString(text, keyBytes);
        }

        public static string EncryptString(string text, byte[] key)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var bytes = Encrypt(textBytes, key);
            return Convert.ToBase64String(bytes);
        }

        public static string DecryptString(string text, byte[] key)
        {
            var textBytes = Convert.FromBase64String(text);
            var bytes = Decrypt(textBytes, key);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
