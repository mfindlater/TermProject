using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Mzsoft.BCrypt;

namespace SocialNetwork
{
    public class EncryptionManager
    { 
        private static string salt = "$2a$10$W1g98wmY8D2kbaXL9SUSze";

        public static string HashPassword(string password)
        {
            return BCrypt.HashPassword(password, salt);
        }

        public static bool CheckPassword(string password,string hashedPassword)
        {
            return BCrypt.CheckPassword(password, hashedPassword);
        }


        public static string Encode(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                output += (char)(input[i] + 3);
            }

            return output;
        }

        public static string Decode(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                output += (char)(input[i] - 3);
            }

            return output;
        }
    }
}
