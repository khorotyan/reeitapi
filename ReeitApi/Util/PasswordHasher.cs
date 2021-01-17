using ReeitApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ReeitApi.Util
{
    public sealed class PasswordHasher
    {
        private const int SALT_SIZE = 32;
        private const int HASH_SIZE = 32;
        private const int ITERATION_NUM = 16384;
        private const string EXTRA_HASH_INFO_BASE = "T5i3_I3-#AS5-V3#Si07_No-1$";

        public static string Hash(string password)
        {
            // Create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SALT_SIZE]);

            // Create hash
            var rfc = new Rfc2898DeriveBytes(password, salt, ITERATION_NUM);
            var hash = rfc.GetBytes(HASH_SIZE);

            // Combine salt and hash
            var hashBytes = new byte[SALT_SIZE + HASH_SIZE];
            Array.Copy(salt, 0, hashBytes, 0, SALT_SIZE);
            Array.Copy(hash, 0, hashBytes, SALT_SIZE, HASH_SIZE);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return $"{EXTRA_HASH_INFO_BASE}{ITERATION_NUM}${base64Hash}";
        }

        public static bool IsHashSupported(string hash)
        {
            return hash.Contains(EXTRA_HASH_INFO_BASE);
        }

        public static bool Verify(string password, string hashedPassword)
        {
            // Check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new ApiException(ErrorCode.Forbidden);
            }

            // Extract iteration and Base64 string
            var hashParts = hashedPassword
                .Replace(EXTRA_HASH_INFO_BASE, "")
                .Split('$');
            var iterations = int.Parse(hashParts[0]);
            var base64Hash = hashParts[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SALT_SIZE];
            Array.Copy(hashBytes, 0, salt, 0, SALT_SIZE);

            // Create hash with the given salt
            var rfc = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = rfc.GetBytes(HASH_SIZE);

            // Get result
            for (int i = 0; i < HASH_SIZE; i++)
            {
                if (hashBytes[i + SALT_SIZE] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
