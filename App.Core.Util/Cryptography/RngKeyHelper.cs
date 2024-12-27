namespace App.Core.Util.Cryptography
{
    /// <summary>
    /// Provides methods for generating cryptographic keys.
    /// </summary>
    public static class RngKeyHelper
    {
        /// <summary>
        /// Generates a cryptographic key of the specified size.
        /// </summary>
        /// <param name="size">The size of the key in bytes. Default is 32 bytes.</param>
        /// <returns>A base64 encoded string representing the generated key.</returns>
        public static string GenerateKey(int size = 32)
        {
            var keyBytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }
    }
}