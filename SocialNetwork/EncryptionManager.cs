using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SocialNetwork
{
    public class EncryptionManager
    {
        private static readonly byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private static readonly byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

        public static string Encrypt(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            var rm = new RijndaelManaged();
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, rm.CreateEncryptor(key, vector), CryptoStreamMode.Write);

            cs.Write(inputBytes, 0, inputBytes.Length);
            cs.FlushFinalBlock();

            ms.Position = 0;
            byte[] encryptedBytes = new byte[ms.Length];

            ms.Read(encryptedBytes, 0, encryptedBytes.Length);

            cs.Close();
            ms.Close();

            string output = Convert.ToBase64String(encryptedBytes);

            return output;
        }

        public static string Decrypt(string input)
        {
            var rm = new RijndaelManaged();
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, rm.CreateDecryptor(key, vector), CryptoStreamMode.Write);

            byte[] encryptedBytes = Convert.FromBase64String(input);

            cs.Write(encryptedBytes, 0, encryptedBytes.Length);
            cs.FlushFinalBlock();

            ms.Position = 0;
            byte[] decryptedBytes = new byte[ms.Length];
            ms.Read(decryptedBytes, 0, decryptedBytes.Length);

            cs.Close();
            ms.Close();

            string output = Encoding.UTF8.GetString(decryptedBytes);

            return output;
        }
    }
}
