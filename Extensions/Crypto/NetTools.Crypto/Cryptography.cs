using System.Security.Cryptography;
using System.Text;

namespace NetTools.Crypto;

public static class Cryptography
{
    private static readonly uint[] Lookup32 = CreateLookup32();

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
    private static byte[] AsByteArray(this string str, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        return encoding.GetBytes(str);
    }

    /// <summary>
    ///     Convert a byte array to a hex string.
    /// </summary>
    /// <param name="bytes">Byte array to convert to hex string.</param>
    /// <returns>Hex string</returns>
    private static string AsHexString(this IReadOnlyList<byte> bytes)
    {
        // Fastest safe way to convert a byte array to hex string,
        // per https://stackoverflow.com/a/624379/13343799

        var lookup32 = Lookup32;
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
    public static string AsHexString(this string str, Encoding? encoding = null)
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
    public static string AsString(this byte[] bytes, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        return encoding.GetString(bytes);
    }

    public static class Hashing
    {
        public static class Standard
        {
            private static string? GenerateHashString(HashAlgorithm algorithm, string text)
            {
                // Compute hash from text parameter
                algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Get has value in array of bytes
                var result = algorithm.Hash;

                // Return as hexadecimal string
                return string.Join(
                    string.Empty,
                    result.Select(x => x.ToString("x2")));
            }

            public static string? Md5(string text)
            {
                var algorithm = MD5.Create();
                return GenerateHashString(algorithm, text);
            }

            public static string? Sha1(string text)
            {
                var algorithm = SHA1.Create();
                return GenerateHashString(algorithm, text);
            }

            public static string? Sha256(string text)
            {
                var algorithm = SHA256.Create();
                return GenerateHashString(algorithm, text);
            }

            public static string? Sha384(string text)
            {
                var algorithm = SHA384.Create();
                return GenerateHashString(algorithm, text);
            }

            public static string? Sha512(string text)
            {
                var algorithm = SHA512.Create();
                return GenerateHashString(algorithm, text);
            }
        }

        public static class Bcrypt
        {
            public static string Hash(string text, string? salt = null)
            {
                return salt != null ? BCrypt.Net.BCrypt.HashPassword(text, salt) : BCrypt.Net.BCrypt.HashPassword(text);
            }

            public static string EnhancedHash(string text)
            {
                return BCrypt.Net.BCrypt.EnhancedHashPassword(text);
            }

            public static bool Verify(string text, string hashToMatch)
            {
                return BCrypt.Net.BCrypt.Verify(text, hashToMatch);
            }

            public static bool EnhancedVerify(string text, string hashToMatch)
            {
                return BCrypt.Net.BCrypt.EnhancedVerify(text, hashToMatch);
            }
        }

        public static class Hmac
        {
            /// <summary>
            ///     Calculate the HMAC-SHA256 hex digest of a byte array.
            /// </summary>
            /// <param name="data">Data to calculate hex digest for.</param>
            /// <param name="secret">Key used to calculate data hex digest.</param>
            /// <param name="normalizationForm">Normalization type to use when normalizing key. Default: No normalization.</param>
            /// <returns>Hex digest of data.</returns>
            public static string CalculateHmacSha256HexDigest(byte[] data, string secret, NormalizationForm? normalizationForm = null)
            {
                if (normalizationForm != null)
                {
                    secret = secret.Normalize(normalizationForm.Value);
                }

                var keyBytes = Encoding.UTF8.GetBytes(secret);

                using var hmac = new HMACSHA256(keyBytes);
                var hash = hmac.ComputeHash(data);

                return hash.AsHexString();
            }

            /// <summary>
            ///     Check whether two signatures match. This is safe against timing attacks.
            /// </summary>
            /// <param name="signature1">First signature.</param>
            /// <param name="signature2">Second signature.</param>
            /// <returns>Whether the two signatures match.</returns>
            public static bool SignaturesMatch(byte[] signature1, byte[]? signature2)
            {
                // short-circuit if second signature is null
                if (signature2 == null)
                {
                    return false;
                }

                // short-circuit if signatures are not the same length
                if (signature1.Length != signature2?.Length)
                {
                    return false;
                }

                var err = false;
                for (var i = 0; i < signature1.Length; i++)
                {
                    if (signature1[i] != signature2[i])
                    {
                        err = true;
                    }
                }

                return !err;
            }

            /// <summary>
            ///     Check whether two signatures match. This is safe against timing attacks.
            /// </summary>
            /// <param name="signature1">First signature.</param>
            /// <param name="signature2">Second signature.</param>
            /// <returns>Whether the two signatures match.</returns>
            public static bool SignaturesMatch(string signature1, string? signature2)
            {
                var signatureBytes1 = signature1.AsByteArray();
                var signatureBytes2 = signature2?.AsByteArray();

                return SignaturesMatch(signatureBytes1, signatureBytes2);
            }
        }
    }

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

    public static class Uuid
    {
        public static Guid GenerateUuid()
        {
            return System.Guid.NewGuid();
        }
    }
}
