namespace App.Core.Util.Cryptography
{
    /// <summary>
    /// Provides helper methods for generating salts, hashing passwords, and verifying hashes using BCrypt.
    /// </summary>
    public static class BCHelper
    {
        /// <summary>
        /// Generates a salt for use in hashing.
        /// </summary>
        public static string GenerateSalt => BC.GenerateSalt();

        /// <summary>
        /// Hashes a password using the provided salt and SHA512 algorithm.
        /// </summary>
        /// <param name="inputKey">The password to hash.</param>
        /// <param name="salt">The salt to use in the hashing process.</param>
        /// <returns>The hashed password.</returns>
        public static string HashPassword(string inputKey, string salt) => BC.HashPassword(inputKey, salt, true, BCrypt.Net.HashType.SHA512);

        /// <summary>
        /// Verifies that the provided text matches the hashed value.
        /// </summary>
        /// <param name="text">The text to verify.</param>
        /// <param name="hash">The hash to compare against.</param>
        /// <returns>True if the text matches the hash; otherwise, false.</returns>
        public static bool Verify(string text, string hash) => BC.Verify(text, hash);
    }
}
