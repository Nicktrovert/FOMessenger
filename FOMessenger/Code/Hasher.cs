using System.Security.Cryptography;

namespace FOMessenger.Code
{
    public static class Hasher
    {
        private const int saltSize = 16;
        private const int keySize = 32;
        private const int iterations = 100000;
        private const char segmentDelimiter = ':';

        private static readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA256;

        public static string Hash(string input)
        {
            // generate salt for uniqueness of hash
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);

            // generate a hash using Password-Based Key Derivation Function 2
            // uses both salt and multiple iterations for added security
            // per RFC 2898 by the Internet Engineering Task Force (IETF), obsoleted by RFC 8018
            // reference https://datatracker.ietf.org/doc/html/rfc2898
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                keySize
            );

            // return the full Hashed password with hash, salt, iteration count and the used algorithm (Important for Verify)
            return string.Join(
                segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algorithm
            );
        }

        public static bool Verify(string input, string hashedPassword)
        {
            // split the hashed password into hash, salt, iteration count and the used algorithm
            string[] segments = hashedPassword.Split(segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int _iterations = int.Parse(segments[2]);
            HashAlgorithmName _algorithm = new HashAlgorithmName(segments[3]);

            // generate a hash of the input using Pbkdf2 using the same arguments that were used for the hashed password
            byte[] HashedInput = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                _iterations,
                _algorithm,
                hash.Length
            );

            // check if both byte sequences have the same length and contents
            return CryptographicOperations.FixedTimeEquals(HashedInput, hash);
        }
    }
}