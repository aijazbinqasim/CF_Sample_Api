namespace App.Core.Util.Cryptography
{
    /// <summary>
    /// Provides methods for encrypting and decrypting text using AES encryption.
    /// </summary>
    public static class CipherHelper
    {
        /// <summary>
        /// Encrypts the specified text using AES encryption.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <returns>The encrypted text as a base64 string.</returns>
        public static string Encrypt(string plainText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(plainText);
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes("Cryp@@)@#", [0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76], 10000, HashAlgorithmName.SHA256);
            if (encryptor == null) return plainText;
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }
            plainText = Convert.ToBase64String(ms.ToArray());
            return plainText;
        }

        /// <summary>
        /// Decrypts the specified encrypted text using AES encryption.
        /// </summary>
        /// <param name="encryptedText">The encrypted text as a base64 string.</param>
        /// <returns>The decrypted text.</returns>
        public static string Decrypt(string encryptedText)
        {
            var cipherBytes = Convert.FromBase64String(encryptedText);
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes("Cryp@@)@#", [0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76], 10000, HashAlgorithmName.SHA256);
            if (encryptor == null) return encryptedText;
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }
            encryptedText = Encoding.Unicode.GetString(ms.ToArray());
            return encryptedText;
        }
    }
}
