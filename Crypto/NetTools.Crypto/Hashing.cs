using System.Security.Cryptography;
using System.Text;
using NetTools.Common.Conversions;

namespace NetTools.Common.Crypto;

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

            if (result == null)
                return null;

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

            return hash.ToHexString();
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
            var signatureBytes1 = signature1.ToByteArray();
            var signatureBytes2 = signature2?.ToByteArray();

            return SignaturesMatch(signatureBytes1, signatureBytes2);
        }
    }
}
