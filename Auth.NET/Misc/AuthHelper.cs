using System;
using System.Security.Cryptography;
using System.Text;

namespace Auth.NET.Misc
{
    internal static class AuthHelper
    {
        internal static string RandomDataBase64Url(uint length)
        {
            var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            return Base64UrlEncodeNoPadding(bytes);
        }

        internal static byte[] Sha256(string inputString)
        {
            var bytes = Encoding.ASCII.GetBytes(inputString);
            var sha256 = new SHA256Managed();
            return sha256.ComputeHash(bytes);
        }

        internal static string Base64UrlEncodeNoPadding(byte[] buffer)
        {
            var base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }
    }
}