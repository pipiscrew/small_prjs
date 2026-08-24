using System;
using System.Security.Cryptography;
using System.Text;

namespace sampleOAuth2
{

    public class PkceGenerator
    {
        public static string CreateCodeVerifier()
        {
            // Generate 32 bytes of cryptographically secure random data
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[32];
                rng.GetBytes(bytes);

                // Format as a URL-safe Base64 string
                return Base64UrlEncode(bytes);
            }
        }

        public static string CreateCodeChallenge(string codeVerifier)
        {
            // Hash the verifier string using SHA-256
            using (var sha256 = SHA256.Create())
            {
                byte[] challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));

                // Format the hash as a URL-safe Base64 string
                return Base64UrlEncode(challengeBytes);
            }
        }

        private static string Base64UrlEncode(byte[] input)
        {
            // Convert to standard Base64 string
            string base64 = Convert.ToBase64String(input);

            // Convert standard Base64 to URL-safe Base64 per RFC 7636
            return base64.Split('=')[0]   // Remove trailing padding '='
                         .Replace('+', '-') // Replace '+' with '-'
                         .Replace('/', '_'); // Replace '/' with '_'
        }
    }

}