using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SocialNetwork
{
    public class EncryptionManager
    {
        private readonly byte[] key = new byte[] { 118, 123, 23, 17, 161, 152, 35, 68, 126, 213, 16, 115, 68, 217, 58, 108, 56, 218, 5, 78, 28, 128, 113, 208, 61, 56, 10, 87, 187, 162, 233, 38 };
        private readonly byte[] iv = { 33, 241, 14, 16, 103, 18, 14, 248, 4, 54, 18, 5, 60, 76, 16, 191 };

        public string Encrypt(string password)
        {
            string encrypted = "";

            using (var rm = new RijndaelManaged())
            {
                rm.Key = key;
                rm.IV = iv;
                ICryptoTransform encryptor = rm.CreateEncryptor(rm.Key, rm.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            //Write all data to the stream.
                            sw.Write(password);
                        }
                        encrypted = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            return encrypted;
        }

        public string Decrypt(string encryptedPassword)
        {
            string decrypted = "";

            using (var rm = new RijndaelManaged())
            {
                rm.Key = key;
                rm.IV = iv;
                ICryptoTransform decrypter = rm.CreateDecryptor(rm.Key, rm.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, decrypter, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            //Read all data from the stream.
                           decrypted = sr.ReadToEnd();
                        }
                    }
                }
            }
            return decrypted;
        }
    }
}
