using ShopifyBackendChallenge.Web.Models;
using System;
using System.Security.Cryptography;

namespace ShopifyBackendChallenge.Web.Utils
{
    public class PasswordUtil
    {
        public static HashSalt GenerateSaltedHash(string password)
        {
            byte[] saltBytes = new byte[64];
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            string hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            HashSalt hashSalt = new HashSalt
            {
                Hash = hashPassword,
                Salt = salt
            };
            return hashSalt;
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == hash;
        }
    }
}
