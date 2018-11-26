using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SocialNetwork
{
    public class EncryptionManager
    { 
        private static readonly int padding = 3;

        public static string Encode(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                output += (char)(input[i] + padding);
            }

            return output;
        }

        public static string Decode(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                output += (char)(input[i] - padding);
            }

            return output;
        }
    }
}
